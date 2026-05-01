# API Agent - DiplomDb.API

## Overview
The API Agent serves as the presentation layer of the DiplomDb system, exposing RESTful endpoints for client applications. It handles HTTP communication, request validation, and response formatting.

## Agent Details

### Basic Information
- **Agent Name**: API Agent
- **Location**: `DiplomDb.API/`
- **Type**: Presentation Layer / Web API
- **Technology Stack**: ASP.NET Core 10.0, Swagger/OpenAPI, AutoMapper, FluentValidation

### Responsibilities
1. **HTTP Endpoint Management**: Expose RESTful API endpoints for scenarios and actions
2. **Input Validation**: Validate incoming requests using FluentValidation
3. **Dependency Injection**: Configure service dependencies through DI container
4. **Documentation**: Provide Swagger/OpenAPI documentation
5. **CORS**: Configure CORS policies for web and mobile clients
6. **Middleware Pipeline**: Configure request/response pipeline, handle exceptions and logging

## Project Structure
```
DiplomDb.API/
├── Controllers/           # API controllers
│   ├── ActionController.cs
│   ├── ScenarioController.cs
│   ├── ObjectController.cs
│   ├── StepController.cs
│   ├── ScenarioStepController.cs
│   └── SessionController.cs
├── Services/             # Business service implementations
│   ├── ActionService.cs
│   ├── ScenarioService.cs
│   ├── ObjectService.cs
│   ├── StepService.cs
│   ├── ScenarioStepService.cs
│   └── SessionService.cs
├── Mapping/              # Object mapping profiles
│   └── MappingProfile.cs
├── Properties/           # Launch settings
│   └── launchSettings.json
├── Program.cs           # Application entry point
├── Dockerfile          # Container configuration
└── appsettings.json    # Configuration files
```

## Key Components

### 1. Controllers
- **ActionController**: Manages CRUD operations for Action entities
- **ScenarioController**: Manages CRUD operations for Scenario entities
- **ObjectController**, **StepController**, **ScenarioStepController**, **SessionController**: Manage respective entities

### 2. Services
Business service implementations that orchestrate domain logic and data access.

### 3. Mapping
- **MappingProfile**: AutoMapper profile for object transformations between DTOs and Domain entities.

## Dependencies

### Internal Dependencies
- **DataAccess Agent** (`DiplomDB.DataAccess`): For data persistence operations
- **DTO Agent** (`Diplom.DTO`): For request/response models and validation

### External Dependencies
- **AutoMapper** (16.1.1): Object-object mapping
- **FluentValidation.AspNetCore** (11.3.1): Input validation
- **Swashbuckle.AspNetCore** (10.1.7): API documentation
- **Microsoft.NET.Sdk.Web**: ASP.NET Core framework

## Configuration

### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### CORS Configuration
- **Policy**: Restrictive policy for `chsufittrack.ru` domain
- **AllowMobileApp**: Permissive policy for mobile applications

### Swagger Configuration
- **Title**: "Diplom DB - база"
- **Version**: "v1"
- **Description**: "API для дипломной работы"

## API Endpoints

### Scenario Endpoints
- `GET /api/scenario`: Retrieve all scenarios
- `GET /api/scenario/{id}`: Retrieve specific scenario
- `POST /api/scenario`: Create new scenario
- `PUT /api/scenario/{id}`: Update existing scenario
- `DELETE /api/scenario/{id}`: Delete scenario

### Action Endpoints
- `GET /api/action`: Retrieve all actions
- `GET /api/action/{id}`: Retrieve specific action
- `POST /api/action`: Create new action
- `PUT /api/action/{id}`: Update existing action
- `DELETE /api/action/{id}`: Delete action

## Development Guidelines

### Adding New Controllers
1. Create new controller in `Controllers/` directory
2. Inherit from `ControllerBase`
3. Add appropriate route attributes
4. Inject required services through constructor
5. Implement action methods with proper HTTP verbs

### Request Validation
1. Create DTO in DTO Agent project
2. Create validator in DTO Agent project
3. Use `[FromBody]` attribute in controller actions
4. Validation happens automatically via FluentValidation

## Deployment

### Docker
Multi-stage Dockerfile included for containerized deployment.

### Environment Variables
- `ASPNETCORE_ENVIRONMENT`: Development/Production
- `ASPNETCORE_URLS`: Server URLs (default: http://+:5007)

---

*Last Updated: 2026-05-01*
*Agent Version: 1.0*