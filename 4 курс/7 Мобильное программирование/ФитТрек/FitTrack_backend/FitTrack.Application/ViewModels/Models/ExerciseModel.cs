
namespace FitTrack.Application.ViewModels.Models;

public class ExerciseModel
{
    public string Id { get; set; } = null!;         // PK e.g., 'bench_press'
    public string NameKey { get; set; } = null!;
    public string LogType { get; set; } = null!;

    public IEnumerable<MuscleGroupModel>? muscleGroups { get; set; }
}
