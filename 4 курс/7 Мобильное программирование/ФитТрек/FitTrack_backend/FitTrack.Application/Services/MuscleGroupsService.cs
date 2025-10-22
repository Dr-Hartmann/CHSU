using AutoMapper;
using FitTrack.Application.Interfaces;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.Services;

internal class MuscleGroupsService(IMuscleGroupRepository muscleGroupRepository, IMapper mapper) : IMuscleGroupsService
{
    public async Task<Result<IEnumerable<MuscleGroupModel>>> GetAllAsync(CancellationToken token)
    {
        var muscleGroups = await muscleGroupRepository.GetAsync(token);
        return Result<IEnumerable<MuscleGroupModel>>.Success(mapper.Map<IEnumerable<MuscleGroupModel>>(muscleGroups));
    }
}
