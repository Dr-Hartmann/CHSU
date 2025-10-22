using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.SeedData;

public class ExercisesSeed(IExerciseRepository exerciseRepository) : IDataSeeder
{
    public int Order => 2;

    public async Task SeedAsync()
    {
        var exercises = await exerciseRepository.GetAsync();
        if (exercises.Count() != 0) return;

        exercises = new List<ExerciseEntity>()
        {
            // Chest
            ExerciseEntity.Create("bench_press", "exercises.bench_press", "weight"),
            ExerciseEntity.Create("incline_dumbbell_press", "exercises.incline_dumbbell_press", "weight"),
            ExerciseEntity.Create("dips", "exercises.dips", "weight"),
            ExerciseEntity.Create("cable_flys", "exercises.cable_flys", "weight"),
            ExerciseEntity.Create("push_ups", "exercises.push_ups", "weight"),
        
            // Back
            ExerciseEntity.Create("pull_ups", "exercises.pull_ups", "weight"),
            ExerciseEntity.Create("deadlift", "exercises.deadlift", "weight"),
            ExerciseEntity.Create("bent_over_rows", "exercises.bent_over_rows", "weight"),
            ExerciseEntity.Create("lat_pulldowns", "exercises.lat_pulldowns", "weight"),
            ExerciseEntity.Create("t_bar_rows", "exercises.t_bar_rows", "weight"),

            // Shoulders
            ExerciseEntity.Create("overhead_press", "exercises.overhead_press", "weight"),
            ExerciseEntity.Create("lateral_raises", "exercises.lateral_raises", "weight"),
            ExerciseEntity.Create("face_pulls", "exercises.face_pulls", "weight"),
            ExerciseEntity.Create("arnold_press", "exercises.arnold_press", "weight"),
        
            // Traps
            ExerciseEntity.Create("shrugs", "exercises.shrugs", "weight"),

            // Biceps
            ExerciseEntity.Create("bicep_curls", "exercises.bicep_curls", "weight"),
            ExerciseEntity.Create("hammer_curls", "exercises.hammer_curls", "weight"),
            ExerciseEntity.Create("chin_ups", "exercises.chin_ups", "weight"),
        
            // Triceps
            ExerciseEntity.Create("tricep_pushdowns", "exercises.tricep_pushdowns", "weight"),
            ExerciseEntity.Create("skull_crushers", "exercises.skull_crushers", "weight"),
            ExerciseEntity.Create("close_grip_bench_press", "exercises.close_grip_bench_press", "weight"),
        
            // Forearms
            ExerciseEntity.Create("wrist_curls", "exercises.wrist_curls", "weight"),

            // Abs & Obliques
            ExerciseEntity.Create("crunches", "exercises.crunches", "weight"),
            ExerciseEntity.Create("leg_raises", "exercises.leg_raises", "weight"),
            ExerciseEntity.Create("plank", "exercises.plank", "timed"),
            ExerciseEntity.Create("russian_twists", "exercises.russian_twists", "weight"),

            // Glutes
            ExerciseEntity.Create("hip_thrusts", "exercises.hip_thrusts", "weight"),
        
            // Quads
            ExerciseEntity.Create("squats", "exercises.squats", "weight"),
            ExerciseEntity.Create("leg_press", "exercises.leg_press", "weight"),
            ExerciseEntity.Create("lunges", "exercises.lunges", "weight"),

            // Hamstrings
            ExerciseEntity.Create("leg_curls", "exercises.leg_curls", "weight"),
            ExerciseEntity.Create("romanian_deadlift", "exercises.romanian_deadlift", "weight"),

            // Calves
            ExerciseEntity.Create("calf_raises", "exercises.calf_raises", "weight"),

            // Cardio
            ExerciseEntity.Create("running", "exercises.running", "cardio"),
            ExerciseEntity.Create("cycling", "exercises.cycling", "cardio")
        };

        await exerciseRepository.CreateAsync(exercises);
    }
}
