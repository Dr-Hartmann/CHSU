namespace FitTrack.Application.ViewModels.Models;

public class MuscleGroupModel
{
    public string Id { get; set; } = null!; // e.g., 'chest'
    public string NameKey { get; set; } = null!; // Reference to I18N key

    public IEnumerable<ExerciseModel>? exercises { get; set; }
}
