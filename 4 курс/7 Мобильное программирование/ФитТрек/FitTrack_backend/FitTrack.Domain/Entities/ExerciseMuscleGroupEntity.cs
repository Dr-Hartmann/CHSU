namespace FitTrack.Domain.Entities;

public class ExerciseMuscleGroupEntity
{
    public string ExerciseId { get; private set; } = null!;
    public string MuscleGroupId { get; private set; } = null!;
    public bool IsPrimary { get; private set; } // Defines the main target muscle

    public ExerciseEntity Exercise { get; private set; } = null!;
    public MuscleGroupEntity MuscleGroup { get; private set; } = null!;

    private ExerciseMuscleGroupEntity() { }

    public static ExerciseMuscleGroupEntity Create(ExerciseEntity exercise,
        MuscleGroupEntity muscleGroup, bool isPrimary = false)
    {
        ArgumentNullException.ThrowIfNull(exercise);
        ArgumentNullException.ThrowIfNull(muscleGroup);

        return new()
        {
            Exercise = exercise,
            ExerciseId = exercise.Id,
            MuscleGroup = muscleGroup,
            MuscleGroupId = muscleGroup.Id,
            IsPrimary = isPrimary,
        };
    }

    public ExerciseMuscleGroupEntity SetAsPrimary()
    {
        IsPrimary = true;
        return this;
    }

    public ExerciseMuscleGroupEntity SetAsSecondary()
    {
        IsPrimary = false;
        return this;
    }
}
