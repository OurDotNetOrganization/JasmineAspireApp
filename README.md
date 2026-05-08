# AspireApp1

AspireApp1 is a .NET Aspire sample solution with:
- `AspireApp1.Web` (Blazor web frontend)
- `AspireApp1.ApiService` (Minimal API backend)
- `AspireApp1.AppHost` (Aspire orchestrator)

The app includes a Jasmine hybrid profile page powered by backend APIs, plus contact lead submission and admin-protected profile update endpoints.

## Prerequisites

- .NET SDK 10
- (Optional) Visual Studio / VS Code / Cursor

## Run the full app (recommended)

```bash
dotnet run --project "AspireApp1.AppHost/AspireApp1.AppHost.csproj"
```

Then open the Aspire dashboard and launch the web/API resources from there.

## Run API only

```bash
dotnet run --project "AspireApp1.ApiService/AspireApp1.ApiService.csproj"
```

Default development URLs:
- `https://localhost:7537`
- `http://localhost:5389`

## Swagger / OpenAPI

In Development, the API exposes:
- Swagger UI:
  - `https://localhost:7537/swagger`
  - `http://localhost:5389/swagger`
- OpenAPI document:
  - `https://localhost:7537/openapi/v1.json`
  - `http://localhost:5389/openapi/v1.json`

## Key API endpoints

### Public
- `GET /api/profile/public` - Read public profile data
- `POST /api/contact-leads` - Submit partnership/contact inquiry

### Admin (JWT + Admin role required)
- `PUT /api/profile/admin` - Replace public profile payload
- `GET /api/contact-leads` - List submitted leads

### Development helper
- `POST /api/auth/token` - Mint a development admin token (Development only)

## JWT configuration

JWT settings are in:
- `AspireApp1.ApiService/appsettings.json` (`Jwt:Issuer`, `Jwt:Audience`, `Jwt:Key`)
- `AspireApp1.ApiService/appsettings.Development.json` (`Jwt:DevAdminUsername`, `Jwt:DevAdminPassword`)

Important:
- Replace development secrets for real use.
- Store production secrets with User Secrets / Key Vault.

## API smoke tests

Use:
- `AspireApp1.ApiService/AspireApp1.ApiService.http`

This file includes requests for:
- Public profile fetch
- Contact lead submission
- Dev token generation
- Admin lead listing with bearer token

## Web integration

The Jasmine page (`/jasmine`) is backed by API clients:
- `ProfileApiClient`
- `ContactApiClient`

Profile content is loaded from `GET /api/profile/public`, and the inquiry form posts to `POST /api/contact-leads`.

## Build

```bash
dotnet build "AspireApp1.slnx"
```

If build fails with file-lock errors, stop running API/AppHost processes and build again.