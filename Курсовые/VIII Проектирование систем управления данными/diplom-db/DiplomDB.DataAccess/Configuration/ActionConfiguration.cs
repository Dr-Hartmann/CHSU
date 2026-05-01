using DiplomDb.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomDB.DataAccess.Configuration;

/// <summary>
/// Конфигурация Entity Framework Core для сущности ActionEntity.
///
/// Назначение файла:
///   - Определение схемы базы данных для таблицы действий (actions)
///   - Настройка свойств, ограничений и отношений для ActionEntity
///   - Применение бизнес-правил на уровне базы данных
///
/// Используемые паттерны и приёмы:
///   1. Fluent API конфигурации EF Core - декларативная настройка маппинга
///   2. Наследование от BaseEntityConfiguration<T> - повторное использование базовой конфигурации
///   3. Каскадное удаление (Cascade Delete) - автоматическое удаление зависимых записей
///   4. Ограничения базы данных (максимальная длина, обязательные поля)
///   5. Настройка отношений один-ко-многим через Fluent API
///
/// Архитектурный слой: DataAccess (Доступ к данным)
/// Ответственный агент: DataAccess Agent
///
/// Примечание: Конфигурация применяется автоматически через ApplyConfigurationsFromAssembly
/// в ApplicationDbContext, обеспечивая централизованное управление схемой БД.
/// </summary>
internal class ActionConfiguration : BaseEntityConfiguration<ActionEntity>
{
    /// <summary>
    /// Настраивает маппинг сущности ActionEntity на таблицу базы данных.
    /// </summary>
    /// <param name="builder">Построитель конфигурации для ActionEntity.</param>
    public override void Configure(EntityTypeBuilder<ActionEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("actions");

        // Настройка свойства Name
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);
    }
}
