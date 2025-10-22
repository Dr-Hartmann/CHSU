

namespace FitTrack.Application.SeedData;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly IEnumerable<IDataSeeder> _seeders;

    public DatabaseInitializer(IEnumerable<IDataSeeder> seeders)
    {
        _seeders = seeders.OrderBy(s => s.Order);
    }

    public async Task InitializeAsync()
    {
        foreach (var seeder in _seeders)
        {
            await seeder.SeedAsync();
        }
    }
}
