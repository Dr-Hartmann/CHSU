# Agents Overview - DiplomDb System

## System Architecture

The DiplomDb system follows a clean, layered architecture with clear separation of concerns. Each layer acts as an "agent" with specific responsibilities and interfaces.

## Agents (Layers)

### 1. **API Agent** (`DiplomDb.API`)
- **Location**: `DiplomDb.API/`
- **Type**: Presentation Layer / Web API
- **Responsibilities**: RESTful endpoints, HTTP handling, input validation, Swagger documentation, object mapping
- **Dependencies**: DataAccess, DTO
- **Key Files**: Controllers (`ActionController.cs`, `ScenarioController.cs`, etc.), `Program.cs`, `appsettings.json`, `MappingProfile.cs`, Services

### 2. **Domain Agent** (`DiplomDb.Domain`)
- **Location**: `DiplomDb.Domain/`
- **Type**: Business Logic Layer
- **Responsibilities**: Domain entities, business rules, repository interfaces, specifications
- **Dependencies**: None (pure domain)
- **Key Files**: Entity classes (`ActionEntity.cs`, `ScenarioEntity.cs`, etc.), Repository interfaces, Specifications

### 3. **DataAccess Agent** (`DiplomDB.DataAccess`)
- **Location**: `DiplomDB.DataAccess/`
- **Type**: Data Persistence Layer
- **Responsibilities**: Database context, repository implementations, entity configurations, migrations
- **Dependencies**: Domain
- **Key Files**: `ApplicationDbContext.cs`, Configuration classes, Repository implementations

### 4. **DTO Agent** (`Diplom.DTO`)
- **Location**: `Diplom.DTO/`
- **Type**: Data Transfer Objects Layer
- **Responsibilities**: Request/response models, validation with FluentValidation
- **Dependencies**: None (data contracts only)
- **Key Files**: `Request.cs`, `Response.cs`, Validation classes

## Agent Communication Flow

```
Client → API Agent → DTO Agent → Domain Agent → DataAccess Agent → Database
      ↑          ↓          ↓          ↑
      ← Mapping (in API) ←─┘          │
```

### Request Flow:
1. **Client** sends HTTP request to **API Agent**
2. **API Agent** validates input using **DTO Agent** validators
3. **API Agent** uses mapping to convert DTO to Domain entity
4. **Domain Agent** applies business rules
5. **DataAccess Agent** persists/retrieves data from database
6. Mapping converts Domain entity back to DTO
7. **API Agent** returns DTO as HTTP response

## Database Schema

The system manages the following entities:
- **Scenario**: Main business scenarios
- **Action**: Actions within scenarios
- **Step**: Individual steps in scenarios
- **ScenarioStep**: Junction table for scenario-step relationships
- **Session**: User sessions
- **Object**: System objects

## Domain Specifications

The Domain Agent includes specification classes that encapsulate query logic using the Specification pattern (Ardalis.Specification). These specifications provide reusable, composable query definitions.

### Key Specifications:
- **`ActionsByIdsSpec`**: Retrieves actions by their IDs
- **`ScenariosWithActionsSpec`**: Retrieves scenarios with their steps and actions
- **`ActiveEntitiesSpec<T>`**: Generic specification for retrieving non-deleted entities
- **`ScenariosByParentIdSpec`**: Retrieves scenarios by parent scenario ID
- **`StepsByActionIdSpec`**: Retrieves steps by action ID
- **`ObjectsByNameSpec`**: Retrieves objects by name (contains search)
- **`SessionsByScenarioIdSpec`**: Retrieves sessions by scenario ID
- **`ScenarioStepsByScenarioIdSpec`**: Retrieves scenario steps by scenario ID, ordered by step order
- **`StepsWithActionsAndObjectsSpec`**: Retrieves steps with related actions and objects

## Development Guidelines

### Adding New Features
1. Define entities in **Domain Agent**
2. Create DTOs in **DTO Agent** with validators
3. Implement repositories in **DataAccess Agent**
4. Add mapping in **API Agent** (`MappingProfile.cs`)
5. Add endpoints in **API Agent**

### Testing Strategy
- Unit tests for **Domain Agent** business logic
- Integration tests for **DataAccess Agent**
- API tests for **API Agent** endpoints
- Validation tests for **DTO Agent**

## Deployment

- **Docker Support**: Multi-stage Dockerfile in API Agent
- **CI/CD**: Gitflic CI pipeline configured for automated builds and tests

---

*Last Updated: 2026-05-01*
*Architecture Version: 1.2*