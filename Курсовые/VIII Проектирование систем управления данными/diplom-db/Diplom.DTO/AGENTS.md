# DTO Agent - Diplom.DTO

## Overview
The DTO Agent serves as the data transfer object layer of the DiplomDb system. It defines request/response models for API communication and implements validation logic using FluentValidation.

## Agent Details

### Basic Information
- **Agent Name**: DTO Agent
- **Location**: `Diplom.DTO/`
- **Type**: Data Transfer Objects Layer / Contract Layer
- **Technology Stack**: C# Records, FluentValidation, .NET 10.0

### Responsibilities
1. **Data Contract Definition**: Define request models for API input and response models for API output
2. **Input Validation**: Implement validation rules using FluentValidation
3. **API Contract Management**: Maintain consistency in API request/response formats

## Project Structure
```
Diplom.DTO/
├── Request.cs              # API request models
├── Response.cs             # API response models
├── Validation/            # Validation logic
│   ├── CreateScenarioValidator.cs
│   └── IValidationMarker.cs
└── Diplom.DTO.csproj
```

## Key Components

### Request Models
Request models represent data sent from clients to the API.

**CreateScenarioRequest** (Record):
```csharp
public record CreateScenarioRequest(
    string Name,
    List<Guid> ActionIds
);
```

### Response Models
Response models represent data returned from the API to clients.

**ActionResponse** (Record):
```csharp
public record ActionResponse(
    Guid Id,
    string Action
);
```

**ScenarioResponse** (Record):
```csharp
public record ScenarioResponse(
    Guid Id,
    string Name,
    List<ActionResponse> Actions
);
```

## Validation
The DTO Agent uses FluentValidation for comprehensive input validation.

### CreateScenarioValidator
Validates `CreateScenarioRequest` objects with rules for Name and ActionIds.

### Validation Marker Interface
`IValidationMarker` serves as a marker interface for assembly scanning.

## Dependencies

### Internal Dependencies
- **None**: DTO Agent should have minimal dependencies (self-contained data contracts and validation)

### External Dependencies
- **FluentValidation**: Validation framework
- **FluentValidation.AspNetCore**: ASP.NET Core integration
- **.NET Standard/Core**: Base framework

---

*Last Updated: 2026-05-01*
*Agent Version: 1.0*