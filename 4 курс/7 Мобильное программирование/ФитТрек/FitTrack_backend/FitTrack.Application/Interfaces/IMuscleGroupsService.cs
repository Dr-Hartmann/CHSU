using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface IMuscleGroupsService
{
    public Task<Result<IEnumerable<MuscleGroupModel>>> GetAllAsync(CancellationToken token = default);
}
