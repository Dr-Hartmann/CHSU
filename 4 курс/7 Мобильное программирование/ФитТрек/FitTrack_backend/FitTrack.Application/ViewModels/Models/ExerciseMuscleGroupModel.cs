namespace FitTrack.Application.ViewModels.Models;

public class ExerciseMuscleGroupModel
{
    public string ExerciseId { get; set; } = null!;
    public string MuscleGroupId { get; set; } = null!;
    public bool IsPrimary { get; set; } // Defines the main target muscle
}
