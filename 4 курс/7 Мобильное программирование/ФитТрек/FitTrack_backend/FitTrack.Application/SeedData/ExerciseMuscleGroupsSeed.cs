using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.SeedData;

public class ExerciseMuscleGroupsSeed(
    IExerciseMuscleGroupRepository exerciseMuscleGroupRepository,
    IExerciseRepository exerciseRepository,
    IMuscleGroupRepository muscleGroupRepository) : IDataSeeder
{
    public int Order => 3;

    public async Task SeedAsync()
    {
        var entities = await exerciseMuscleGroupRepository.GetAsync();
        if (entities.Count() != 0) return;

        // Get all exercises and muscle groups once
        var exercises = await exerciseRepository.GetAsync();
        var muscleGroups = await muscleGroupRepository.GetAsync();

        // Create dictionaries for quick lookup
        var exerciseDict = exercises.ToDictionary(e => e.Id, e => e);
        var muscleGroupDict = muscleGroups.ToDictionary(m => m.Id, m => m);

        var exerciseMuscleGroups = new List<ExerciseMuscleGroupEntity>();

        // Helper method to add relationships
        void AddRelationship(string exerciseId, string muscleGroupId, bool isPrimary = false)
        {
            if (!exerciseDict.ContainsKey(exerciseId))
                throw new Exception($"Exercise {exerciseId} not found");

            if (!muscleGroupDict.ContainsKey(muscleGroupId))
                throw new Exception($"Muscle group {muscleGroupId} not found");

            var exerciseMuscleGroupEntity = ExerciseMuscleGroupEntity.Create
            (
                exerciseDict[exerciseId],
                muscleGroupDict[muscleGroupId],
                isPrimary
            );

            exerciseMuscleGroups.Add(exerciseMuscleGroupEntity);
        }

        // Chest exercises
        AddRelationship("bench_press", "chest", true);
        AddRelationship("bench_press", "triceps");
        AddRelationship("bench_press", "shoulders");

        AddRelationship("incline_dumbbell_press", "chest", true);
        AddRelationship("incline_dumbbell_press", "shoulders");

        AddRelationship("dips", "chest", true);
        AddRelationship("dips", "triceps");
        AddRelationship("dips", "shoulders");

        AddRelationship("cable_flys", "chest", true);

        AddRelationship("push_ups", "chest", true);
        AddRelationship("push_ups", "triceps");
        AddRelationship("push_ups", "shoulders");

        // Back exercises
        AddRelationship("pull_ups", "back", true);
        AddRelationship("pull_ups", "biceps");

        AddRelationship("deadlift", "back", true);
        AddRelationship("deadlift", "hamstrings");
        AddRelationship("deadlift", "glutes");
        AddRelationship("deadlift", "quads");

        AddRelationship("bent_over_rows", "back", true);
        AddRelationship("bent_over_rows", "biceps");

        AddRelationship("lat_pulldowns", "back", true);
        AddRelationship("lat_pulldowns", "biceps");

        AddRelationship("t_bar_rows", "back", true);
        AddRelationship("t_bar_rows", "biceps");

        // Shoulders exercises
        AddRelationship("overhead_press", "shoulders", true);
        AddRelationship("overhead_press", "triceps");

        AddRelationship("lateral_raises", "shoulders", true);

        AddRelationship("face_pulls", "shoulders", true);
        AddRelationship("face_pulls", "back");

        AddRelationship("arnold_press", "shoulders", true);
        AddRelationship("arnold_press", "triceps");

        // Traps exercises
        AddRelationship("shrugs", "traps", true);

        // Biceps exercises
        AddRelationship("bicep_curls", "biceps", true);
        AddRelationship("bicep_curls", "forearms");

        AddRelationship("hammer_curls", "biceps", true);
        AddRelationship("hammer_curls", "forearms");

        AddRelationship("chin_ups", "biceps", true);
        AddRelationship("chin_ups", "back");

        // Triceps exercises
        AddRelationship("tricep_pushdowns", "triceps", true);

        AddRelationship("skull_crushers", "triceps", true);

        AddRelationship("close_grip_bench_press", "triceps", true);
        AddRelationship("close_grip_bench_press", "chest");

        // Forearms exercises
        AddRelationship("wrist_curls", "forearms", true);

        // Abs & Obliques exercises
        AddRelationship("crunches", "abs", true);

        AddRelationship("leg_raises", "abs", true);
        AddRelationship("leg_raises", "obliques");

        AddRelationship("plank", "abs", true);
        AddRelationship("plank", "obliques");
        AddRelationship("plank", "shoulders");
        AddRelationship("plank", "back");

        AddRelationship("russian_twists", "obliques", true);
        AddRelationship("russian_twists", "abs");

        // Glutes exercises
        AddRelationship("hip_thrusts", "glutes", true);
        AddRelationship("hip_thrusts", "hamstrings");

        // Quads exercises
        AddRelationship("squats", "quads", true);
        AddRelationship("squats", "glutes");
        AddRelationship("squats", "hamstrings");

        AddRelationship("leg_press", "quads", true);
        AddRelationship("leg_press", "glutes");
        AddRelationship("leg_press", "hamstrings");

        AddRelationship("lunges", "quads", true);
        AddRelationship("lunges", "glutes");
        AddRelationship("lunges", "hamstrings");

        // Hamstrings exercises
        AddRelationship("leg_curls", "hamstrings", true);

        AddRelationship("romanian_deadlift", "hamstrings", true);
        AddRelationship("romanian_deadlift", "glutes");
        AddRelationship("romanian_deadlift", "back");

        // Calves exercises
        AddRelationship("calf_raises", "calves", true);

        // Cardio exercises
        AddRelationship("running", "calves", true);
        AddRelationship("running", "quads");
        AddRelationship("running", "hamstrings");
        AddRelationship("running", "glutes");

        AddRelationship("cycling", "quads", true);
        AddRelationship("cycling", "calves");
        AddRelationship("cycling", "glutes");
        AddRelationship("cycling", "hamstrings");

        await exerciseMuscleGroupRepository.CreateAsync(exerciseMuscleGroups);
    }
}
