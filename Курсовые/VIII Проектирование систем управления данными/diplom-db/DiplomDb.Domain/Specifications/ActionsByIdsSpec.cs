using Ardalis.Specification;
using DiplomDb.Domain.Entity;

namespace DiplomDb.Domain.Specifications;

/// <summary>
/// Спецификация для получения действий по их идентификаторам.
///
/// Назначение файла:
///   - Инкапсуляция логики запроса для выборки действий по списку ID
///   - Реализация паттерна Specification для повторного использования запросов
///   - Следование принципу Single Responsibility (SRP)
///
/// Используемые паттерны и приёмы:
///   1. Паттерн Specification - инкапсуляция бизнес-правил запросов
///   2. Композиция запросов - возможность комбинирования с другими спецификациями
///   3. Принцип единственной ответственности - класс отвечает только за один тип запроса
///   4. Использование Ardalis.Specification - стандартизированный подход к спецификациям
///
/// Архитектурный слой: Domain (Бизнес-логика)
/// Ответственный агент: Domain Agent
///
/// Пример использования:
///   var spec = new ActionsByIdsSpec(actionIds);
///   var actions = await actionRepository.ListAsync(spec);
/// </summary>
public class ActionsByIdsSpec : Specification<ActionEntity>
{
    /// <summary>
    /// Инициализирует новую спецификацию для выборки действий по идентификаторам.
    /// </summary>
    /// <param name="ids">Коллекция идентификаторов действий для фильтрации.</param>
    public ActionsByIdsSpec(IEnumerable<Guid> ids)
    {
        Query.Where(x => ids.Contains(x.Id));
    }
}