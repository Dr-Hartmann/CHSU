# DataAccess Agent - DiplomDB.DataAccess

## Overview
The DataAccess Agent serves as the data persistence layer of the DiplomDb system. It implements repository patterns, database context, entity configurations, and data access logic using Entity Framework Core.

## Agent Details

### Basic Information
- **Agent Name**: DataAccess Agent
- **Location**: `DiplomDB.DataAccess/`
- **Type**: Data Persistence Layer / Infrastructure Layer
- **Technology Stack**: Entity Framework Core, .NET 10.0, SQL Server/In-Memory Database

### Responsibilities
1. **Database Context Management**: Define DbContext and DbSet properties, manage connections and transactions
2. **Entity Configuration**: Implement EntityTypeConfiguration classes, define database schema and constraints
3. **Repository Implementation**: Implement repository interfaces from Domain layer, provide data access operations
4. **Data Persistence Logic**: Handle save changes interception, audit trail, soft delete functionality

## Project Structure
```
DiplomDB.DataAccess/
├── ApplicationDbContext.cs    # Database context
├── BeautifulDataAccess.cs     # Extension methods
├── Configuration/             # Entity configurations
│   ├── BaseEntityConfiguration.cs
│   ├── ActionConfiguration.cs
│   ├── ScenarioConfiguration.cs
│   ├── StepConfiguration.cs
│   ├── ScenarioStepConfiguration.cs
│   ├── SessionConfiguration.cs
│   └── ObjectConfiguration.cs
├── Repository/               # Repository implementations
│   ├── ActionRepository.cs
│   ├── ScenarioRepository.cs
│   ├── ObjectRepository.cs
│   ├── StepRepository.cs
│   ├── ScenarioStepRepository.cs
│   └── SessionRepository.cs
└── DiplomDB.DataAccess.csproj
```

## Key Components

### 1. ApplicationDbContext
The main DbContext class that manages database sessions and entity tracking.

**Features:**
- Automatic GUID generation for new entities
- Automatic timestamp management (CreatedAt, UpdatedAt)
- Soft delete support via IsDeleted flag
- Configuration discovery via assembly scanning

**DbSets:**
- `DbSet<ActionEntity> Actions`
- `DbSet<ScenarioEntity> Scenarios`
- `DbSet<ObjectEntity> Objects`
- `DbSet<StepEntity> Steps`
- `DbSet<SessionEntity> Sessions`
- `DbSet<ScenarioStepEntity> ScenarioSteps`

### 2. Entity Configurations
Each entity has a corresponding configuration class that defines database mapping.

**BaseEntityConfiguration** (Abstract):
- Configures common properties for all entities
- Sets up primary key, indexes, and soft delete filter
- Applied to all entities inheriting from BaseEntity

**Specific Configurations:**
- **ActionConfiguration**: Maps ActionEntity properties and relationships
- **ScenarioConfiguration**: Maps ScenarioEntity properties and relationships
- **StepConfiguration**: Maps StepEntity properties
- **ScenarioStepConfiguration**: Maps many-to-many relationship
- **SessionConfiguration**: Maps SessionEntity properties
- **ObjectConfiguration**: Maps ObjectEntity properties

### 3. Repository Implementations
Concrete implementations of repository interfaces defined in Domain layer.

**ActionRepository:** Implements IActionRepository interface, provides action-specific data operations, supports specification pattern for queries.

**ScenarioRepository:** Implements IScenarioRepository interface, provides scenario-specific data operations, supports eager loading of related entities.

**Other repositories:** ObjectRepository, StepRepository, ScenarioStepRepository, SessionRepository.

## Database Schema

### Entity Relationships
```
Scenario (1) ─────── (many) Action
    │
    └── (many) ScenarioStep (many) ─── (1) Step
```

### Table Structure
1. **Actions**: `Id` (PK, GUID), `CreatedAt`, `UpdatedAt`, `IsDeleted`, Scenario foreign key and other properties
2. **Scenarios**: `Id` (PK, GUID), `CreatedAt`, `UpdatedAt`, `IsDeleted`, Name, description, and scenario metadata
3. **Steps**: `Id` (PK, GUID), `CreatedAt`, `UpdatedAt`, `IsDeleted`, Step details and instructions
4. **ScenarioSteps** (Junction table): `ScenarioId` (FK, GUID), `StepId` (FK, GUID), additional relationship metadata
5. **Sessions**: `Id` (PK, GUID), `CreatedAt`, `UpdatedAt`, `IsDeleted`, Session state and user information
6. **Objects**: `Id` (PK, GUID), `CreatedAt`, `UpdatedAt`, `IsDeleted`, Object metadata and properties

## Data Access Patterns

### Repository Pattern
- Abstracts data access logic
- Provides consistent interface for data operations
- Supports testability through dependency injection

### Specification Pattern
- Encapsulates query logic in reusable objects
- Supports composition of multiple specifications
- Enables complex query building

### Unit of Work Pattern
- Managed by Entity Framework Core DbContext
- Tracks changes across multiple operations
- Ensures transactional consistency

## Configuration

### Database Providers

#### In-Memory Database (Development)
```csharp
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("DiplomDb"));
```

#### SQL Server (Production)
```csharp
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
```

## Dependencies

### Internal Dependencies
- **Domain Agent** (`DiplomDb.Domain`): For entity definitions and repository interfaces

### External Dependencies
- **Microsoft.EntityFrameworkCore** (EF Core)
- **Microsoft.EntityFrameworkCore.InMemory** (Development)
- **Microsoft.EntityFrameworkCore.SqlServer** (Production)
- **Microsoft.EntityFrameworkCore.Tools** (Migrations)

---

*Last Updated: 2026-05-01*
*Agent Version: 1.0*