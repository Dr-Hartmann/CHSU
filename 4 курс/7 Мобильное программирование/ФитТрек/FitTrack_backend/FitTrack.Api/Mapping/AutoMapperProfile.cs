
using AutoMapper;
using FitTrack.Api.ViewModels.SyncModel;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Api.Mapping;

/// <summary>
/// Конфигурационный профиль AutoMapper для преобразования объектов между слоями приложения
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Конструктор профиля маппинга - настраивает преобразования между ViewModel и Model
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<SyncData, SyncDataModel>();
    }
}
