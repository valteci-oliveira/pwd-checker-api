---
name: testing-pwd-checker-api
description: Test the pwd-checker-api end-to-end. Use when verifying password validation logic, security controls, or API behavior changes.
---

# Testing pwd-checker-api

## Prerequisites

- .NET 10 SDK installed (see blueprint)
- No external services, databases, or credentials required
- This is an API-only project with no frontend UI

## Build & Run

```bash
# Build
dotnet build src/

# Run locally (Development mode — enables Swagger)
dotnet run --project src/pwd-checker-api/pwd-checker-api.csproj
# API: http://localhost:5238
# Swagger: http://localhost:5238/swagger
# Health: http://localhost:5238/health

# Run unit tests
dotnet test src/pwd-checker-api-test/pwd-checker-api-test.csproj --verbosity normal
```

## API Endpoint

Single endpoint: `POST /api/v1/password/validate`

```bash
curl -s -w "\nHTTP_STATUS: %{http_code}\n" \
  -X POST 'http://localhost:5238/api/v1/password/validate' \
  -H 'Content-Type: application/json' \
  -d '{"password": "AbcDef1!"}'
```

**Response codes:**
- 200: Password is valid
- 400: Missing/empty password or exceeds max length (128 chars)
- 422: Password fails validation rules
- 429: Rate limited (30 req/min)

## Validation Rules (Chain of Responsibility)

1. MinLengthHandler — minimum 8 characters
2. NoRepeatCharHandler — no repeated characters
3. LowercaseHandler — at least one lowercase letter
4. UppercaseHandler — at least one uppercase letter
5. SpecialCharHandler — at least one special character
6. DigitHandler — at least one digit

A valid password example: `"AbcDef1!"` (8 chars, all unique, has lower/upper/special/digit)

## Testing Approach

- **No recording needed** — this is API-only, all tests are shell-based curl requests
- Test via curl against the running local server
- Check HTTP status codes and JSON response bodies for pass/fail
- For rate limiting tests, send rapid requests in a loop and check for HTTP 429
- For CORS tests, send OPTIONS preflight requests with different `Access-Control-Request-Method` headers
- For timing/performance tests, measure response time with `date +%s%N` before/after curl
- Rate limiter uses a 1-minute fixed window — wait 60s between rate limit tests to reset

## Docker

```bash
# Dev mode (port 5000 -> 5238)
docker-compose up

# Prod mode (port 9000 -> 5238)
docker-compose -f docker-compose.prod.yml up -d
```

Note: Healthcheck runs inside the container on port 5238 (the internal port), not the host-mapped port.

## Devin Secrets Needed

None — this project has no external dependencies or authentication requirements.
