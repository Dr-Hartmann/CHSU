using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface ITemplateExerciseRepository : ICRUDRepository<TemplateExerciseEntity>
{
    Task<IEnumerable<TemplateExerciseEntity>> GetByTemplateExerciseGroupIdAsync(Guid templateExGroupId);
    Task<TemplateExerciseEntity?> GetByIdAsync(Guid id);
    Task RemoveAsync(Guid id, CancellationToken token = default);
}
