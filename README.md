# Subscription Service — ASP.NET Core 8 Web API

Сервис управления подписками: регистрация/авторизация (JWT), управление подписками, аналитика расходов.
Clean Architecture, EF Core (SQLite in-memory → PostgreSQL одной строкой конфигурации), xUnit + Moq + FluentAssertions.

**Состояние:** `dotnet build` — 0 warnings / 0 errors; **103 теста — все зелёные**;
покрытие тестируемых сервисов — **100% statements / 100% branches**; smoke-тесты пройдены на живом API.

---

## 1. Быстрый старт

```bash
dotnet restore
dotnet build
dotnet test                                     # 103 теста
dotnet run --project src/SubscriptionService.Api # http://localhost:8080 → /swagger
```

Переменные запуска (пример):

```bash
ASPNETCORE_URLS=http://0.0.0.0:8080 ASPNETCORE_ENVIRONMENT=Development \
  dotnet run --project src/SubscriptionService.Api
```

* Swagger UI: `http://localhost:8080/swagger` (корень `/` редиректит туда), health: `/health`.
* Демо-пользователь (seed): **demo@subscriptions.local / Demo1234!** — 6 платежей за 6 месяцев
  (Basic 3×10 $, Premium 3×25 $, итого 105 $), есть отменённая и активная подписки.
* Весь код одним документом: [`FULL_SOURCE.md`](FULL_SOURCE.md).
* Готовый скрипт smoke-тестов: `./smoke-test.sh` (нужны `curl`, `jq`).

## 2. Структура решения

```
SubscriptionService.sln
Directory.Build.props                  # net8.0, nullable, analyzers — общие настройки
src/
  SubscriptionService.Domain/          # ядро: сущности + инварианты, ZERO зависимостей
    Entities/{User,Subscription,Payment}.cs
    Enums/{SubscriptionType,SubscriptionStatus,PaymentStatus}.cs
    Exceptions/AppExceptions.cs
  SubscriptionService.Application/     # use-cases: сервисы, DTO, абстракции (порты)
    Abstractions/{IClock,ICurrentUser,IPasswordHasher,IJwtTokenGenerator}.cs
    Abstractions/Persistence/{IUserRepository,ISubscriptionRepository,IPaymentRepository,IUnitOfWork}.cs
    Abstractions/Services/{IAuthService,ISubscriptionService,IAnalyticsService}.cs
    Services/{AuthService,SubscriptionService,AnalyticsService}.cs
    Pricing/{ISubscriptionPricing,SubscriptionPricing}.cs
    DTOs/{AuthDtos,SubscriptionDtos,AnalyticsDtos}.cs
    Mappings/DtoMappings.cs, Options/{JwtOptions,PricingOptions}.cs
    Common/SubscriptionExpense.cs, DependencyInjection.cs
  SubscriptionService.Infrastructure/  # адаптеры: EF Core, Identity PasswordHasher, JWT
    Persistence/AppDbContext.cs, Persistence/Configurations/*
    Persistence/Repositories/*, Persistence/EfUnitOfWork.cs, Persistence/DbInitializer.cs
    Security/{IdentityPasswordHasher,JwtTokenGenerator}.cs, Common/SystemClock.cs
    DatabaseOptions.cs, DependencyInjection.cs
  SubscriptionService.Api/             # HTTP: контроллеры, middleware, Program.cs
    Controllers/{Auth,Subscriptions,Analytics}Controller.cs
    Middleware/ExceptionHandlingMiddleware.cs, Services/HttpCurrentUser.cs
    Program.cs, appsettings*.json
tests/
  SubscriptionService.UnitTests/       # xUnit + Moq + FluentAssertions + coverlet
    Services/{SubscriptionService,AuthService,AnalyticsService,SubscriptionPricing}Tests.cs
    Domain/{Subscription,PaymentAndUser}Tests.cs, Common/TestData.cs
```

Направление зависимостей: `Api → Infrastructure → Application → Domain`.
Domain не знает ни про EF, ни про HTTP; Application знает только свои интерфейсы (порты), Infrastructure их реализует (адаптеры).

## 3. API

| Метод | Маршрут | Доступ | Описание |
|---|---|---|---|
| POST | `/api/auth/register` | анонимно | Email + Password + Name → JWT (200), 400 валидация, 409 дубль e-mail |
| POST | `/api/auth/login` | анонимно | JWT (200), 401 при неверных данных |
| GET | `/api/subscriptions` | JWT | подписки с доступом (Active + Cancelled до конца оплаченного периода) |
| POST | `/api/subscriptions` | JWT | создать подписку + первый платёж **в одной транзакции** (201) |
| DELETE | `/api/subscriptions/{id}` | JWT | отмена: `Status = Cancelled`, доступ до `currentPeriodEnd` (200) |
| GET | `/api/analytics/expenses?months=6` | JWT | расходы по месяцам и типам, 1–36 месяцев |
| GET | `/health` | анонимно | health check |

Ошибки — единый RFC 7807 `application/problem+json` c полями `code` (`validation_error`, `not_found`, `conflict`, `unauthorized`, …) и `traceId`.

## 4. Бизнес-правила

* **Цены за месяц:** Basic 10 $, Premium 25 $, Corporate 100 $ (`Pricing` в конфиге).
  Стоимость периода = цена месяца × `BillingPeriodInMonths` (Premium × 6 = 150 $).
* **Период:** `BillingPeriodInMonths` ∈ [1; 12]; `StartDate` — не в будущем, нормализуется к UTC-полночи;
  `CurrentPeriodEnd = StartDate.AddMonths(months)` — эксклюзивная граница оплаченного доступа.
* **Одна активная подписка на пользователя.** Пока есть `Active` и не истёк период — `409 Conflict`.
  После отмены можно сразу оформить новый тариф (смена плана), доступ по старому сохраняется до конца периода.
* **Отмена не укорачивает оплаченный период:** `CancelledAtUtc` фиксируется, `CurrentPeriodEnd` не меняется, `HasAccess = true`.
  Повторная отмена / отмена истёкшей подписки — `409`.
* **Чужая подписка = 404** (не 403), чтобы не раскрывать существование чужих id.
* **Аналитика** строится по фактическим платежам (`PaymentStatus.Succeeded`), месяцы без платежей заполняются нулями,
  границы — календарные месяцы, включая текущий; деньги округляются до 2 знаков (`MidpointRounding.AwayFromZero`).
* Статус `Expired` не хранится — истечение вычисляется из `CurrentPeriodEnd`, поэтому фоновый job не нужен для корректности данных.

## 5. Безопасность

* Пароли — `PasswordHasher<User>` из ASP.NET Core Identity (PBKDF2-HMAC-SHA256, 100 000 итераций, соль на каждый пароль).
  В домене хранится только `PasswordHash`.
* JWT HS256: claims `sub`, `email`, `jti`, `name`; проверка issuer / audience / lifetime / signing key, `ClockSkew = 30 c`, `MapInboundClaims = false`.
* `JwtOptions` валидируется на старте (`ValidateDataAnnotations` + `ValidateOnStart`), ключ ≥ 32 символа.
  В проде — `dotnet user-secrets` / переменные окружения (`Jwt__SigningKey`), а не appsettings.
* Все эндпоинты закрыты: `[Authorize]` на контроллерах **плюс** `FallbackPolicy = RequireAuthenticatedUser`;
  `[AllowAnonymous]` только на `AuthController`, `/health`, `/`.
* Логин не раскрывает, существует ли аккаунт: одно сообщение для «нет пользователя» и «неверный пароль».

## 6. Транзакции

`SubscriptionService.CreateAsync` открывает `IUnitOfWork.BeginTransactionAsync()`, пишет подписку и платёж,
`SaveChangesAsync` → `CommitAsync`; при любом исключении — `RollbackAsync` + `await using` (dispose).
Отмена — один `SaveChangesAsync`, он атомарен сам по себе.

`EfUnitOfWork` проверяет `Database.IsRelational()`: для SQLite/PostgreSQL это настоящая транзакция
(в логах EF видно `Beginning transaction … Serializable → Creating savepoint → Committing`),
для EF InMemory — корректный no-op вместо `InvalidOperationException`.

## 7. Провайдеры БД

`appsettings.json → Database:Provider`:

| Значение | Что это | Когда |
|---|---|---|
| `SqliteInMemory` (по умолчанию) | настоящий SQL + транзакции, `DataSource=:memory:`, соединение живёт всё время процесса | запуск/демо/тесты без установки чего-либо |
| `InMemory` | EF InMemory, транзакции деградируют в no-op | быстрые тесты |
| `Postgres` | Npgsql, нужен `Database:ConnectionString` | прод |

Переключение на PostgreSQL:

```bash
export Database__Provider=Postgres
export Database__ConnectionString="Host=localhost;Port=5432;Database=subscriptions;Username=postgres;Password=postgres"
dotnet tool install --global dotnet-ef
dotnet ef migrations add Initial --project src/SubscriptionService.Infrastructure --startup-project src/SubscriptionService.Api
dotnet ef database update --project src/SubscriptionService.Infrastructure --startup-project src/SubscriptionService.Api
dotnet run --project src/SubscriptionService.Api
```

Модель уже готова к Npgsql: `decimal(18,2)` для денег, enum'ы строками, конвертер `DateTime → UTC`
(`ConfigureConventions`), уникальные индексы и каскады заданы в `IEntityTypeConfiguration`.
В проде заменить `EnsureCreated()` на `Database.Migrate()` и выключить `Database:SeedDemoData`.

## 8. Тесты и покрытие

```bash
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults
```

103 теста (xUnit + Moq + FluentAssertions), все зависимости сервисов — моки, время заморожено (`IClock`).

| Класс | Statements | Branches |
|---|---|---|
| `SubscriptionService` | 94/94 = **100%** | 28/28 = **100%** |
| `AuthService` | 76/76 = **100%** | 42/42 = **100%** |
| `AnalyticsService` | 50/50 = **100%** | 10/10 = **100%** |
| `SubscriptionPricing` | 9/9 = **100%** | 4/4 = **100%** |
| **Итого по сервисам** | **229/229 = 100%** | **84/84 = 100%** |

Цель ТЗ (>80% ветвлений, >90% операторов) перекрыта. Покрытые сценарии:

* **Создание:** успех (цена, период, платёж, commit), порядок вызовов `begin → add sub → add payment → save → commit`,
  все тарифы (Theory), прошлая/будущая/пустая дата, `months` = 0/−1/13/100, неизвестный тип, несколько ошибок сразу,
  отсутствие пользователя (404), уже активная подписка (409), сбой `SaveChanges`/платежей → **rollback без commit**, проброс `CancellationToken`.
* **Отмена:** успех с сохранением доступа, чужая/несуществующая подписка (404), повторная отмена (409), истёкший период (409), отсутствие лишней транзакции.
* **Список:** Active + Cancelled с доступом, сортировка, пустой список.
* **Auth:** хеширование и нормализация e-mail, дубль (409), слабый пароль/имя/e-mail, null-поля, логин/неверный пароль/нет пользователя.
* **Аналитика:** группировка месяц × тип, нулевые месяцы, границы диапазона, переход через год, округление, `months` 0/−1/37, 1 и 36 месяцев.
* **Домен:** инварианты `Subscription`/`Payment`/`User`, граница доступа (`end − 1 tick` / `end`), `Renew`, повторная отмена.

## 9. Smoke-тесты (curl)

```bash
BASE=http://localhost:8080

# 1) регистрация → JWT
TOKEN=$(curl -s -X POST "$BASE/api/auth/register" -H "Content-Type: application/json" \
  -d '{"email":"smoke@example.com","password":"Passw0rd!","name":"Smoke Tester"}' | jq -r .accessToken)

# 2) создание подписки Premium на 6 месяцев (150 $)
curl -s -X POST "$BASE/api/subscriptions" -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \
  -d '{"type":"Premium","startDate":"2026-11-11","billingPeriodInMonths":6}' | jq .

# 3) отчёт о расходах за 6 месяцев
curl -s "$BASE/api/analytics/expenses?months=6" -H "Authorization: Bearer $TOKEN" | jq .
```

Проверено на живом сервисе: 201/150 $/`currentPeriodEnd = 2027-05-11`; отчёт демо-пользователя — 105 $ за 6 месяцев;
401 без токена, 400 при невалидном теле, 409 при дубле e-mail и повторной отмене, 404 для чужой подписки.

## 10. Что стоит добавить в проде

Фоновый job продления (`Renew` уже в домене) + идемпотентные ключи платежей, refresh-токены и revoke,
rate limiting на `/api/auth/*`, `Database.Migrate()` + миграции, outbox для платёжного провайдера,
интеграционные тесты на `WebApplicationFactory<Program>` (точка входа уже `public partial class Program`),
OpenTelemetry-трейсинг и structured logging.