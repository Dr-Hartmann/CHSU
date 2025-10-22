
namespace FitTrack.Application.SeedData;

public interface IDataSeeder
{
    int Order { get; }
    Task SeedAsync();
}
