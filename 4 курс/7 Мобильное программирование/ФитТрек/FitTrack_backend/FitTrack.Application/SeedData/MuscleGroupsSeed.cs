using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.SeedData;

public class MuscleGroupsSeed(IMuscleGroupRepository muscleGroupRepository) : IDataSeeder
{
    public int Order => 1;

    public async Task SeedAsync()
    {
        var existing = await muscleGroupRepository.GetAsync();
        var existingIds = existing.Select(x => x.Id).ToHashSet(StringComparer.OrdinalIgnoreCase);

        var allGroups = new List<MuscleGroupEntity>()
        {
            MuscleGroupEntity.Create(id: "chest", nameKey: "muscleGroups.chest" ),
            MuscleGroupEntity.Create(id: "back", nameKey: "muscleGroups.back" ),
            MuscleGroupEntity.Create(id: "shoulders", nameKey: "muscleGroups.shoulders" ),
            MuscleGroupEntity.Create(id: "traps", nameKey: "muscleGroups.traps" ),
            MuscleGroupEntity.Create(id: "biceps", nameKey: "muscleGroups.biceps" ),
            MuscleGroupEntity.Create(id: "triceps", nameKey: "muscleGroups.triceps" ),
            MuscleGroupEntity.Create(id: "forearms", nameKey: "muscleGroups.forearms" ),
            MuscleGroupEntity.Create(id: "abs", nameKey: "muscleGroups.abs" ),
            MuscleGroupEntity.Create(id: "obliques", nameKey: "muscleGroups.obliques" ),
            MuscleGroupEntity.Create(id: "glutes", nameKey: "muscleGroups.glutes" ),
            MuscleGroupEntity.Create(id: "quads", nameKey: "muscleGroups.quads" ),
            MuscleGroupEntity.Create(id: "hamstrings", nameKey: "muscleGroups.hamstrings" ),
            MuscleGroupEntity.Create(id: "calves", nameKey: "muscleGroups.calves" ),
        };

        var toInsert = allGroups.Where(g => !existingIds.Contains(g.Id)).ToList();
        if (toInsert.Count > 0)
            await muscleGroupRepository.CreateAsync(toInsert);
    }
}
