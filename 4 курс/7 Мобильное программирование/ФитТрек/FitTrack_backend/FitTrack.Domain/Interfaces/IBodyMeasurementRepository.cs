using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface IBodyMeasurementRepository : ICRUDRepository<BodyMeasurementEntity>
{
    Task<BodyMeasurementEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<BodyMeasurementEntity>> GetByUserIdAsync(int userId);
    Task<BodyMeasurementEntity?> GetByUserIdAndDateAsync(int userId, DateTime date);
    Task<IEnumerable<BodyMeasurementEntity>> GetByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
}
