using AutoMapper;
using Diplom.DTO;
using DiplomDb.Domain.Entity;

namespace DiplomDb.API;

/// <summary>
/// Профиль AutoMapper для преобразования объектов между слоями приложения.
///
/// Назначение файла:
///   - Определение правил преобразования между доменными сущностями и DTO
///   - Инкапсуляция логики маппинга в одном месте
///   - Обеспечение согласованности преобразований данных
///
/// Используемые паттерны и приёмы:
///   1. AutoMapper Profile - централизованная конфигурация маппингов
///   2. Fluent configuration API - декларативное определение правил преобразования
///   3. Separation of Concerns - отделение логики маппинга от бизнес-логики и контроллеров
///   4. Custom member mappings - сложные преобразования с использованием ForMember()
///   5. LINQ projections - оптимизация запросов через ProjectTo<T>()
///
/// Архитектурный слой: 
/// (Слой преобразования объектов)
/// Ответственный агент: mapper Agent
///
/// Примечание: Профиль регистрируется в DI контейнере и используется автоматически
/// через внедрение IMapper в контроллерах и сервисах.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр профиля маппинга с настройкой всех преобразований.
    /// </summary>
    public MappingProfile()
    {
        // Маппинг ActionEntity -> ActionResponse
        // Простое прямое преобразование свойств
        CreateMap<ActionEntity, ActionResponse>();
        
        // Маппинг ScenarioEntity -> ScenarioResponse
        // Сложное преобразование с извлечением действий из связанных сущностей
        CreateMap<ScenarioEntity, ScenarioResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserRequest))
            .ForMember(dest => dest.Actions, opt => opt.MapFrom(src => src.ScenarioSteps
                .OrderBy(ss => ss.Order)
                .Select(ss => ss.Step.Action)));
        
        // Маппинг ObjectEntity -> ObjectResponse
        CreateMap<ObjectEntity, ObjectResponse>();
        
        // Маппинг StepEntity -> StepResponse
        // Включает навигационные свойства Action и Object
        CreateMap<StepEntity, StepResponse>()
            .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
            .ForMember(dest => dest.Object, opt => opt.MapFrom(src => src.Object));
        
        // Маппинг ScenarioStepEntity -> ScenarioStepResponse
        CreateMap<ScenarioStepEntity, ScenarioStepResponse>()
            .ForMember(dest => dest.Scenario, opt => opt.MapFrom(src => src.Scenario))
            .ForMember(dest => dest.Step, opt => opt.MapFrom(src => src.Step));
        
        // Маппинг SessionEntity -> SessionResponse
        CreateMap<SessionEntity, SessionResponse>()
            .ForMember(dest => dest.Scenario, opt => opt.MapFrom(src => src.Scenario));
    }
}
