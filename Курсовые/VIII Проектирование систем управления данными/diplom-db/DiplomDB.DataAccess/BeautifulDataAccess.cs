using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;
using DiplomDB.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace DiplomDB.DataAccess;

public static class BeautifulDataAccess
{
    public static IServiceCollection AddBeautifulDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database")
            ?? throw new InvalidOperationException("Connection string 'Database' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        return WithDependencies(services);
    }

    public static IServiceCollection MakeDbContextInMemory(this IServiceCollection services)
    {
        var databaseName = $"DiplomDbInMemory_{Guid.NewGuid():N}";

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("DiplomDbInMemory"));

        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .UseInMemoryDatabase(databaseName)
        );

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            WithSeedData(context);
        }

        return WithDependencies(services);
    }

    private static void WithSeedData(ApplicationDbContext context)
    {
        // 1. Create Actions
        var action1 = ActionEntity.Create("Сесть");
        var action2 = ActionEntity.Create("Встать");
        var action3 = ActionEntity.Create("Притоптать");
        var action4 = ActionEntity.Create("Открыть");
        var action5 = ActionEntity.Create("Закрыть");
        var action6 = ActionEntity.Create("Включить");

        context.Actions.AddRange(action1, action2, action3, action4, action5, action6);
        context.SaveChanges();

        // 2. Create Objects
        var object1 = ObjectEntity.Create("Стул");
        var object2 = ObjectEntity.Create("Стол");
        var object3 = ObjectEntity.Create("Дверь");
        var object4 = ObjectEntity.Create("Окно");
        var object5 = ObjectEntity.Create("Компьютер");
        var object6 = ObjectEntity.Create("Поле");

        context.Objects.AddRange(object1, object2, object3, object4, object5, object6);
        context.SaveChanges();

        // 3. Create Steps (combining Actions and Objects)
        var step1 = StepEntity.Create(action1.Id, object1.Id); // Сесть на стул
        var step2 = StepEntity.Create(action2.Id, object1.Id); // Встать со стула
        var step3 = StepEntity.Create(action3.Id, object6.Id); // Притоптать поле
        var step4 = StepEntity.Create(action4.Id, object3.Id); // Открыть дверь
        var step5 = StepEntity.Create(action5.Id, object4.Id); // Закрыть окно
        var step6 = StepEntity.Create(action6.Id, object5.Id); // Включить компьютер

        context.Steps.AddRange(step1, step2, step3, step4, step5, step6);
        context.SaveChanges();

        // 4. Create Scenarios - first create parent scenario
        var scenario1 = ScenarioEntity.Create(
            parentScenarioId: null,
            userRequest: "Создать утреннюю рутину",
            llmContext: JsonDocument.Parse(
            """
                {
                    "context": "Утренние процедуры полезны для здоровья",
                    "source_path": "doc/morning.md"
                }
            """)
        );
        context.Scenarios.Add(scenario1);
        context.SaveChanges(); // Save to get Id

        // Now create child scenarios with parentScenarioId = scenario1.Id
        var scenario2 = ScenarioEntity.Create(
            parentScenarioId: scenario1.Id,
            userRequest: "Рабочий процесс",
            llmContext: JsonDocument.Parse("{\"context\": \"Рабочие задачи\"}")
        );
        var scenario3 = ScenarioEntity.Create(
            parentScenarioId: scenario1.Id,
            userRequest: "Физические упражнения",
            llmContext: JsonDocument.Parse("{\"context\": \"Физкультура\"}")
        );

        context.Scenarios.AddRange(scenario2, scenario3);
        context.SaveChanges();

        // 5. Create ScenarioSteps (linking scenarios to steps with order)
        var scenarioStep1 = ScenarioStepEntity.Create(scenario1.Id, step1.Id, order: 1);
        var scenarioStep2 = ScenarioStepEntity.Create(scenario1.Id, step2.Id, order: 2);
        var scenarioStep3 = ScenarioStepEntity.Create(scenario1.Id, step4.Id, order: 3);
        var scenarioStep4 = ScenarioStepEntity.Create(scenario2.Id, step6.Id, order: 1);
        var scenarioStep5 = ScenarioStepEntity.Create(scenario2.Id, step5.Id, order: 2);
        var scenarioStep6 = ScenarioStepEntity.Create(scenario3.Id, step3.Id, order: 1);
        var scenarioStep7 = ScenarioStepEntity.Create(scenario3.Id, step1.Id, order: 2);

        context.ScenarioSteps.AddRange(scenarioStep1, scenarioStep2, scenarioStep3, scenarioStep4,
            scenarioStep5, scenarioStep6, scenarioStep7);
        context.SaveChanges();

        // 6. Create Sessions
        var session1 = SessionEntity.Create(scenario1.Id, "Основной курс");
        var session2 = SessionEntity.Create(scenario2.Id, "Продвинутый курс");
        var session3 = SessionEntity.Create(scenario3.Id, "Физкультурный курс");

        context.Sessions.AddRange(session1, session2, session3);
        context.SaveChanges();
    }

    private static IServiceCollection WithDependencies(IServiceCollection services)
    {
        return services
            .AddScoped<IActionRepository, ActionRepository>()
            .AddScoped<IScenarioRepository, ScenarioRepository>()
            .AddScoped<IObjectRepository, ObjectRepository>()
            .AddScoped<ISessionRepository, SessionRepository>()
            .AddScoped<IStepRepository, StepRepository>()
            .AddScoped<IScenarioStepRepository, ScenarioStepRepository>();
    }
}
