namespace DiplomDb.Domain.Interface;

using Ardalis.Specification;
using DiplomDb.Domain.Entity;

/// <summary>
/// Интерфейс репозитория для работы с сущностью ActionEntity.
///
/// Назначение файла:
///   - Определение контракта репозитория в слое Domain
///   - Абстракция доступа к данным для ActionEntity
///   - Следование принципу Dependency Inversion (DIP)
///
/// Используемые паттерны и приёмы:
///   1. Интерфейс репозитория - абстракция над механизмом хранения данных
///   2. Наследование от IRepositoryBase<T> - использование готовой базовой функциональности
///   3. Принцип разделения интерфейсов (ISP) - минимальный специализированный контракт
///   4. Dependency Inversion Principle - высокоуровневые модули зависят от абстракций
///
/// Архитектурный слой: Domain (Бизнес-логика)
/// Ответственный агент: Domain Agent
///
/// Примечание: Наследует все базовые операции CRUD от IRepositoryBase<T>,
/// что позволяет использовать спецификации (Specification pattern) для сложных запросов.
/// </summary>
public interface IActionRepository : IRepositoryBase<ActionEntity> { }
