namespace Diplom.DTO;

/// <summary>
/// Data Transfer Objects (DTO) для входящих запросов API.
///
/// Назначение файла:
///   - Определение структур данных для входящих HTTP-запросов
///   - Валидация входных данных через FluentValidation
///   - Изоляция API контрактов от доменных моделей
///
/// Используемые паттерны и приёмы:
///   1. Record types (C# 9+) - неизменяемые DTO с value-based equality
///   2. Positional records - компактное объявление с автоматическими свойствами
///   3. Separation of Concerns - отделение запросов API от доменных сущностей
///   4. FluentValidation integration - декларативная валидация через атрибуты или отдельные валидаторы
///
/// Архитектурный слой: DTO (Data Transfer Objects)
/// </summary>

public record CreateScenarioRequest(
    string Name,
    List<Guid> ActionIds
);

public record CreateActionRequest(
    string Name
);

public record CreateObjectRequest(
    string Name
);

public record CreateStepRequest(
    Guid ActionId,
    Guid ObjectId
);

public record CreateScenarioStepRequest(
    Guid ScenarioId,
    Guid StepId,
    int Order
);

public record CreateSessionRequest(
    Guid ScenarioId,
    string CourseName
);
