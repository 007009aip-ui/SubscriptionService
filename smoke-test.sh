#!/usr/bin/env bash
# Smoke tests for the Subscription Service API. Requires: curl, jq.
# Usage: ./smoke-test.sh [base-url]
set -euo pipefail
BASE="${1:-http://localhost:8080}"
EMAIL="smoke+$(date +%s)@example.com"

echo "== 1. REGISTER =="
REG=$(curl -sS -X POST "$BASE/api/auth/register" \
  -H "Content-Type: application/json" \
  -d "{\"email\":\"$EMAIL\",\"password\":\"Passw0rd!\",\"name\":\"Smoke Tester\"}")
echo "$REG" | jq '{tokenType, expiresAtUtc, user}'
TOKEN=$(echo "$REG" | jq -r '.accessToken')

echo "== 2. CREATE SUBSCRIPTION (Premium x 6 months = 150 USD) =="
SUB=$(curl -sS -X POST "$BASE/api/subscriptions" \
  -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \
  -d "{\"type\":\"Premium\",\"startDate\":\"$(date -u +%F)\",\"billingPeriodInMonths\":6}")
echo "$SUB" | jq .
ID=$(echo "$SUB" | jq -r '.id')

echo "== 3. EXPENSE REPORT (last 6 months) =="
curl -sS "$BASE/api/analytics/expenses?months=6" -H "Authorization: Bearer $TOKEN" | jq .

echo "== 4. LIST ACTIVE =="
curl -sS "$BASE/api/subscriptions" -H "Authorization: Bearer $TOKEN" | jq .

echo "== 5. CANCEL (access kept until end of paid period) =="
curl -sS -X DELETE "$BASE/api/subscriptions/$ID" -H "Authorization: Bearer $TOKEN" \
  | jq '{id, status, cancelledAtUtc, currentPeriodEnd, hasAccess}'

echo "== 6. NEGATIVE CHECKS =="
printf 'no token            -> %s (expect 401)\n' "$(curl -s -o /dev/null -w '%{http_code}' "$BASE/api/subscriptions")"
printf 'invalid payload     -> %s (expect 400)\n' "$(curl -s -o /dev/null -w '%{http_code}' -X POST "$BASE/api/subscriptions" -H "Authorization: Bearer $TOKEN" -H 'Content-Type: application/json' -d '{"type":"Premium","startDate":"2020-01-01","billingPeriodInMonths":0}')"
printf 'duplicate e-mail    -> %s (expect 409)\n' "$(curl -s -o /dev/null -w '%{http_code}' -X POST "$BASE/api/auth/register" -H 'Content-Type: application/json' -d "{\"email\":\"$EMAIL\",\"password\":\"Passw0rd!\",\"name\":\"Dup\"}")"
printf 'double cancel       -> %s (expect 409)\n' "$(curl -s -o /dev/null -w '%{http_code}' -X DELETE "$BASE/api/subscriptions/$ID" -H "Authorization: Bearer $TOKEN")"
printf 'months=0            -> %s (expect 400)\n' "$(curl -s -o /dev/null -w '%{http_code}' "$BASE/api/analytics/expenses?months=0" -H "Authorization: Bearer $TOKEN")"
