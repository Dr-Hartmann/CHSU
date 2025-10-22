using AutoMapper;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Entities;

namespace FitTrack.Application.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User
        CreateMap<UserEntity, UserModel>();
        CreateMap<UserModel, UserEntity>();

        // Workout
        CreateMap<WorkoutEntity, WorkoutModel>();
        CreateMap<WorkoutModel, WorkoutEntity>();

        // ExerciseGroup
        CreateMap<ExerciseGroupEntity, ExerciseGroupModel>();
        CreateMap<ExerciseGroupModel, ExerciseGroupEntity>();

        // ExerciseLog
        CreateMap<ExerciseLogEntity, ExerciseLogModel>();
        CreateMap<ExerciseLogModel, ExerciseLogEntity>();

        // SetLog
        CreateMap<SetLogEntity, SetLogModel>();
        CreateMap<SetLogModel, SetLogEntity>();

        // BodyMeasurement
        CreateMap<BodyMeasurementEntity, BodyMeasurementModel>();
        CreateMap<BodyMeasurementModel, BodyMeasurementEntity>();

        // Exercise
        CreateMap<ExerciseEntity, ExerciseModel>();
        CreateMap<ExerciseModel, ExerciseEntity>();

        // MuscleGroup
        CreateMap<MuscleGroupEntity, MuscleGroupModel>();
        CreateMap<MuscleGroupModel, MuscleGroupEntity>();

        // ExerciseMuscleGroup
        CreateMap<ExerciseMuscleGroupEntity, ExerciseMuscleGroupModel>();
        CreateMap<ExerciseMuscleGroupModel, ExerciseMuscleGroupEntity>();

        // WorkoutTemplate
        CreateMap<WorkoutTemplateEntity, WorkoutTemplateModel>();
        CreateMap<WorkoutTemplateModel, WorkoutTemplateEntity>();

        // TemplateExerciseGroup
        CreateMap<TemplateExerciseGroupEntity, TemplateExerciseGroupModel>();
        CreateMap<TemplateExerciseGroupModel, TemplateExerciseGroupEntity>();

        // TemplateExercise
        CreateMap<TemplateExerciseEntity, TemplateExerciseModel>();
        CreateMap<TemplateExerciseModel, TemplateExerciseEntity>();

        // UserAchievement
        CreateMap<UserAchievementEntity, UserAchievementModel>();
        CreateMap<UserAchievementModel, UserAchievementEntity>();

        // AchievementDefinition
        CreateMap<AchievementDefinitionEntity, AchievementDefinitionModel>();
        CreateMap<AchievementDefinitionModel, AchievementDefinitionEntity>();

        // Settings
        CreateMap<SettingsEntity, SettingsModel>();
        CreateMap<SettingsModel, SettingsEntity>();
    }
}
