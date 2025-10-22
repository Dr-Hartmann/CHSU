using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface ITemplateExerciseGroupRepository : ICRUDRepository<TemplateExerciseGroupEntity>
{
    Task<IEnumerable<TemplateExerciseGroupEntity>> GetByTemplateIdAsync(Guid templateId);
    Task<TemplateExerciseGroupEntity?> GetByIdAsync(Guid id);
    Task RemoveAsync(Guid id, CancellationToken token = default);
}
