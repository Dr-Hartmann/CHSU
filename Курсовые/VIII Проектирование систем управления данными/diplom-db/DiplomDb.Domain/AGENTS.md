# Domain Agent - DiplomDb.Domain

## Overview
The Domain Agent represents the core business logic layer of the DiplomDb system. It contains the domain entities, business rules, repository interfaces, and specifications that define the business domain.

## Agent Details

### Basic Information
- **Agent Name**: Domain Agent
- **Location**: `DiplomDb.Domain/`
- **Type**: Business Logic Layer / Domain Layer
- **Technology Stack**: .NET 10.0, C# Domain-Driven Design

### Responsibilities
1. **Domain Modeling**: Define core business entities and value objects
2. **Business Logic**: Encapsulate business rules and validations
3. **Repository Abstraction**: Define repository interfaces for data access
4. **Specification Pattern**: Define reusable query specifications

## Project Structure
```
DiplomDb.Domain/
├── Entity/               # Domain entities
│   ├── BaseEntity.cs
│   ├── ActionEntity.cs
│   ├── ScenarioEntity.cs
│   ├── StepEntity.cs
│   ├── ScenarioStepEntity.cs
│   ├── SessionEntity.cs
│   └── ObjectEntity.cs
├── Interface/           # Repository interfaces
│   ├── IActionRepository.cs
│   ├── IScenarioRepository.cs
│   ├── IObjectRepository.cs
│   ├── IStepRepository.cs
│   ├── IScenarioStepRepository.cs
│   └── ISessionRepository.cs
├── Specifications/      # Query specifications
│   ├── ActionsByIdsSpec.cs
│   ├── ScenariosWithActionsSpec.cs
│   ├── ActiveEntitiesSpec.cs
│   ├── ScenariosByParentIdSpec.cs
│   ├── StepsByActionIdSpec.cs
│   ├── ObjectsByNameSpec.cs
│   ├── SessionsByScenarioIdSpec.cs
│   ├── ScenarioStepsByScenarioIdSpec.cs
│   └── StepsWithActionsAndObjectsSpec.cs
├── Service/            # Domain service interfaces (future)
└── DiplomDb.Domain.csproj
```

## Domain Model

### Core Entities
1. **BaseEntity** (Abstract)
   - `Id`: GUID identifier
   - `CreatedAt`: Creation timestamp
   - `UpdatedAt`: Last update timestamp
   - `IsDeleted`: Soft delete flag

2. **ScenarioEntity**: Represents a business scenario
3. **ActionEntity**: Represents an action within a scenario
4. **StepEntity**: Represents an individual step in scenarios
5. **ScenarioStepEntity**: Junction entity for many-to-many relationship
6. **SessionEntity**: Represents user sessions
7. **ObjectEntity**: Represents system objects

### Entity Relationships
```
Scenario (1) ──┐ (many) ScenarioStep (many) ──┐ (1) Step
               │                              │
               └── (many) Action
```

## Repository Interfaces

Each entity has a corresponding repository interface extending `IRepositoryBase<T>`:
- `IActionRepository`
- `IScenarioRepository`
- `IObjectRepository`
- `IStepRepository`
- `IScenarioStepRepository`
- `ISessionRepository`

## Specifications

Specifications encapsulate query logic in reusable, composable objects using Ardalis.Specification.

### Available Specifications
- **`ActionsByIdsSpec`**: Filters actions by a collection of IDs
- **`ScenariosWithActionsSpec`**: Eager loads scenarios with their related actions
- **`ActiveEntitiesSpec<T>`**: Generic specification for retrieving non-deleted entities
- **`ScenariosByParentIdSpec`**: Retrieves scenarios by parent scenario ID
- **`StepsByActionIdSpec`**: Retrieves steps by action ID
- **`ObjectsByNameSpec`**: Retrieves objects by name (contains search)
- **`SessionsByScenarioIdSpec`**: Retrieves sessions by scenario ID
- **`ScenarioStepsByScenarioIdSpec`**: Retrieves scenario steps by scenario ID, ordered by step order
- **`StepsWithActionsAndObjectsSpec`**: Retrieves steps with related actions and objects

### Usage Example
```csharp
var spec = new ScenariosWithActionsSpec();
var scenarios = await _scenarioRepository.ListAsync(spec);
```

## Business Rules

### Validation Rules
1. **Entity Validation**: All entities must have valid IDs, CreatedAt ≤ UpdatedAt, soft-deleted entities cannot be modified.
2. **Scenario Rules**: Scenarios must have a non-empty name, valid start and end dates, active scenarios cannot be deleted.
3. **Action Rules**: Actions must belong to at least one scenario, action execution order must be sequential, completed actions cannot be modified.

## Development Guidelines

### Adding New Entities
1. Create entity class in `Entity/` directory
2. Inherit from `BaseEntity`
3. Define properties and relationships
4. Implement business rules as methods

### Adding New Repository Interfaces
1. Create interface in `Interface/` directory
2. Define repository contract
3. Follow repository pattern principles

### Creating Specifications
1. Create specification class in `Specifications/` directory
2. Implement specification logic
3. Ensure specifications are composable

## Dependencies

### Internal Dependencies
- **None**: Domain layer should have no external dependencies (pure domain logic)

### External Dependencies
- **.NET Standard/Core**: Base framework
- **System**: Basic .NET types and utilities

---

*Last Updated: 2026-05-01*
*Agent Version: 1.0*