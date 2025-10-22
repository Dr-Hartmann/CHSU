## FitTrack Backend — quick instructions for AI coding agents

These notes focus on actionable, repository-specific knowledge that helps an AI coding agent be productive immediately.

1) Big picture
  - Monorepo with three main layers:
    - `FitTrack.Api/` — ASP.NET Core Web API (controllers, middleware, DI wiring, OpenAPI)
    - `FitTrack.Application/` — application services, DTOs, interfaces, seeders and business logic
    - `FitTrack.DataAccess/` — EF Core `ApplicationDbContext`, repositories and DI helpers
  - Program entrypoint: `FitTrack.Api/Program.cs` (startup, authentication, DI and database seeding)
  - DI helpers use the "MakeBeautiful*" convention: `MakeBeautifulAuthentication`, `MakeBeautifulServices`, `MakeBeautifulDbContext`, `MakeBeautifulServicesForTests`.

2) Typical data flow / patterns
  - Controllers call services from `FitTrack.Application.Services` via interfaces in `FitTrack.Application.Interfaces`.
  - Services use repositories from `FitTrack.DataAccess.Repositories` injected by `BeautifulDataAccess`.
  - DTOs / ViewModels live under `FitTrack.Application.ViewModels` and `FitTrack.Api.ViewModels`.
  - AutoMapper profiles are declared in both API and Application projects (`Mapping/AutoMapperProfile.cs`).

3) Authentication & security
  - JWT configured via `JwtSettingsModel` (see `FitTrack.Application.ViewModels.Models.JwtSettingsModel.cs`).
  - Startup wires JWT in `FitTrack.Api.DI.BeautifulApi.MakeBeautifulAuthentication` and `Program.cs` binds `JwtSettings` from configuration.
  - Swagger/OpenAPI is enabled (see `Program.cs` / `CORRECTED_Program.cs`) and configured to accept Bearer token.

4) Tests and test DI
  - Tests rely on `MakeBeautifulServicesForTests()` which creates an in-memory `ApplicationDbContext`.
  - Unit/integration tests obtain services via a ServiceCollection e.g. in `FitTrack.Tests/UserTests.cs` (see `GetService()` pattern).
  - When adding or updating services make sure to add corresponding service registrations in `FitTrack.Application.DI.BeautifulApplication.AddDependencies`.

5) Important conventions & non-obvious details
  - Database initialization: on startup `IDatabaseInitializer` runs seeders (`FitTrack.Application.SeedData.DatabaseInitializer` and implementations). Changing seed order uses the `Order` property on `IDataSeeder`.
  - For local development `Program.cs` currently uses `MakeBeautifulServicesForTests()` instead of production DB. Switch to `MakeBeautifulServices(builder.Configuration)` to connect to SQL Server.
  - Custom middleware: `FitTrack.Api.Middleware.ExMiddleware` centralizes exception handling and returns ProblemDetails — follow its mapping for status codes.
  - Rate limiting and health checks are shown in `CORRECTED_Program.cs` (example policies: `sync-limiter`, `global-limiter`). If you implement rate-limited endpoints, reuse those policy names.
  - Logging: `FitTrack.DataAccess.DI.BeautifulDataAccess` registers a `ConsoleLoggerProvider` for EF Core; repository logging follows Microsoft.Extensions.Logging patterns.

6) Build / test / format workflows (commands validated in repo README)
  - Check formatting (verify):
    dotnet format ./FitTrack.sln --verify-no-changes
  - Apply formatting:
    dotnet format ./FitTrack.sln
  - Run tests:
    dotnet test ./FitTrack.sln
  - Strict build with analyzers:
    dotnet build ./FitTrack.sln --configuration Release --no-restore /p:RunAnalyzers=true /p:AnalysisLevel=latest

7) Files to reference when making changes
  - Startup and wiring: `FitTrack.Api/Program.cs` and `CORRECTED_Program.cs` (the latter contains useful enhancements)
  - DI registration: `FitTrack.Api/DI/BeautifulApi.cs`, `FitTrack.Application/DI/BeautifulApplication.cs`, `FitTrack.DataAccess/DI/BeautifulDataAccess.cs`
  - Seeders & DB init: `FitTrack.Application/SeedData/*` and `DatabaseInitializer.cs`
  - Middleware: `FitTrack.Api/Middleware/ExMiddleware.cs`
  - Repositories: `FitTrack.DataAccess/Repositories/*` and `FitTrack.Domain/Entities/*`

8) Example code patterns to follow
  - Service registration snippet (follow exactly):
    .AddScoped<IUserService, UserService>()
  - In tests create an in-memory service provider:
    new ServiceCollection().MakeBeautifulServicesForTests().BuildServiceProvider()
  - Reading JWT config in code:
    builder.Services.Configure<JwtSettingsModel>(builder.Configuration.GetSection("JwtSettings"));

9) What NOT to change lightly
  - The `MakeBeautifulServicesForTests()` usage in `Program.cs` — switching to production DB affects local runs and CI. Coordinate with maintainers.
  - The central `ExMiddleware` contract — other code expects ProblemDetails with specific status mappings.

10) Quick troubleshooting tips
  - If tests fail due to DB concurrency or transactions, look at `BeautifulDataAccess.MakeBeautifulDbContextForTests` — it uses an in-memory DB with TransactionIgnoredWarning suppressed.
  - If JWT token validation fails check `appsettings*.json` for `JwtSettings` and ensure `SecretKey` exists.

If anything in these notes looks incomplete or you want examples for a specific change (new controller, service, repository), tell me which area and I'll expand with snippets and exact file locations.
