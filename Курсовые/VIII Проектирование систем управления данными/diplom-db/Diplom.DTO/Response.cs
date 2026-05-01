namespace Diplom.DTO;

/// <summary>
/// Data Transfer Objects (DTO) для исходящих ответов API.
///
/// Назначение файла:
///   - Определение структур данных для HTTP-ответов API
///   - Сериализация доменных сущностей в клиентские форматы
///   - Контроль над данными, экспортируемыми наружу API
///
/// Используемые паттерны и приёмы:
///   1. Record types (C# 9+) - неизменяемые DTO с value-based equality
///   2. Positional records - компактное объявление с автоматическими свойствами
///   3. Separation of Concerns - отделение ответов API от доменных сущностей
///   4. Nullable reference types - явное указание nullable свойств для опциональных связей
///   5. Flattened responses - упрощенные представления сложных графов объектов
///
/// Архитектурный слой: DTO (Data Transfer Objects)
/// </summary>

public record ActionResponse(
    Guid Id,
    string Name
);

public record ScenarioResponse(
    Guid Id,
    string Name,
    List<ActionResponse> Actions
);

public record ObjectResponse(
    Guid Id,
    string Name
);

public record StepResponse(
    Guid Id,
    Guid ActionId,
    Guid ObjectId,
    ActionResponse? Action,
    ObjectResponse? Object
);

public record ScenarioStepResponse(
    Guid Id,
    Guid ScenarioId,
    Guid StepId,
    int Order,
    ScenarioResponse? Scenario,
    StepResponse? Step
);

public record SessionResponse(
    Guid Id,
    Guid ScenarioId,
    string CourseName,
    ScenarioResponse? Scenario
);
