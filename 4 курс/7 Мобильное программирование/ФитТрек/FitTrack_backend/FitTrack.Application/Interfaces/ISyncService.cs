

using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface ISyncService
{
    public Task<Result<SyncDataModel>> SyncAsync(int userId, SyncDataModel data, CancellationToken token = default);
}
