using Ardalis.Specification.EntityFrameworkCore;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;

namespace DiplomDB.DataAccess.Repository;

/// <summary>
/// Реализация репозитория для работы с сущностью ActionEntity.
///
/// Назначение файла:
///   - Конкретная реализация интерфейса IActionRepository в слое DataAccess
///   - Обеспечение доступа к данным ActionEntity в базе данных через EF Core
///   - Инкапсуляция логики работы с базой данных для действий
///
/// Используемые паттерны и приёмы:
///   1. Repository pattern - абстракция доступа к данным
///   2. Dependency Injection через конструктор - внедрение зависимости ApplicationDbContext
///   3. Наследование от RepositoryBase<T> - повторное использование базовой реализации
///   4. Реализация интерфейса IActionRepository - следование контракту Domain слоя
///   5. Primary constructor (C# 12) - компактное объявление зависимостей
///
/// Архитектурный слой: DataAccess (Доступ к данным)
/// Ответственный агент: DataAccess Agent
///
/// Примечание: Наследует все CRUD операции и поддержку спецификаций от RepositoryBase<T>,
/// что обеспечивает согласованность реализации репозиториев во всей системе.
/// </summary>
internal class ActionRepository(ApplicationDbContext context)
    : RepositoryBase<ActionEntity>(context), IActionRepository
{ }
