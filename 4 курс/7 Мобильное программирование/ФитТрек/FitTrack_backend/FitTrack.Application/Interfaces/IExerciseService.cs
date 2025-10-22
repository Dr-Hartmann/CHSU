using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface IExerciseService
{
    public Task<Result<ExerciseModel>> CreateAsync(string id, string nameKey, string logType, CancellationToken token = default);
    public Task<Result<IEnumerable<ExerciseModel>>> GetAllAsync(CancellationToken token = default);
}
