using Xunit.Abstractions;

namespace FitTrack.Tests;

public class UserTests(ITestOutputHelper testOutputHelper)
{
    /*
    [Fact]
    public async Task CreateAndGetUsers()
    {
        var service = GetService();

        await service.CreateRangeAsync(
            [
                new (){ Login = "DungeonMaster69", Password = "69", Name = "Билли Херрингтон" },
                new (){ Login = "KingOfArena", Password = "1", Name = "Рикардо Милос" },
                new (){ Login = "ShadowSamurai", Password = "1", Name = "Ван Даркхолм" },
                new (){ Login = "IronFist", Password = "1", Name = "Джефф Страйкер" },
                new (){ Login = "SilverWolf", Password = "1", Name = "Коби Ди" },
                new (){ Login = "DarkKnight", Password = "1", Name = "Майкл Лушин" },
                new (){ Login = "RedDragon", Password = "1", Name = "Алекс Монтана" },
                new (){ Login = "StormBreaker", Password = "1", Name = "Питер Норт" },
                new (){ Login = "WildBear", Password = "1", Name = "Марк Вульф" },
                new (){ Login = "ThunderGod", Password = "1", Name = "Трой Бонд" }
            ]
        );

        var users = await service.GetAllAsync();

        foreach (var user in users)
        {
            testOutputHelper.WriteLine($"{user.Login}. {user.Name} - '{user.CreatedAt}':'{user.UpdatedAt}'");
        }
        Assert.NotEmpty(users);
        Assert.Equal(10, users.Count());
    }

    [Fact]
    public async Task UpdateUser()
    {
        var service = GetService();

        await service.CreateRangeAsync(
            [
                new (){ Login = "Test", Password = "69", Name = "Test User" },
            ]
        );

        await service.ChangePasswordAsync(
            new() { OldLogin = "Test", NewLogin = "YOOOOOLOOOOOOOO", NewPassword = "1488" }
        );

        var users = await service.GetAllAsync();

        foreach (var user in users)
        {
            testOutputHelper.WriteLine($"{user.Login}. {user.Name} - '{user.CreatedAt}':'{user.UpdatedAt}'");
        }
        Assert.NotEmpty(users);
        Assert.Single(users);
    }

    private static IUserService GetService()
    {
        var provider = new ServiceCollection()
            .MakeBeautifulServicesForTests()
            .BuildServiceProvider();
        return provider.GetRequiredService<IUserService>();
    }
    */
}
