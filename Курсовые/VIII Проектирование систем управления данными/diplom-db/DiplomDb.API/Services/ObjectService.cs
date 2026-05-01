using AutoMapper;
using Diplom.DTO;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;
using DiplomDb.Domain.Specifications;

namespace DiplomDb.API.Services;

public class ObjectService(IObjectRepository repository, IMapper mapper, ILogger<ObjectService> logger)
    : BaseService<ObjectEntity, CreateObjectRequest, ObjectResponse>(repository, mapper, logger), IObjectService
{
    public override async Task<ObjectResponse> CreateAsync(CreateObjectRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Creating new object with name {Name}", request.Name);

        var obj = ObjectEntity.Create(request.Name);
        await repository.AddAsync(obj, cancellationToken);

        logger.LogInformation("Object created with ID {Id}", obj.Id);

        return mapper.Map<ObjectResponse>(obj);
    }

    public override async Task<ObjectResponse> UpdateAsync(Guid id, CreateObjectRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Updating object with ID {Id}", id);

        var obj = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Object with ID {id} not found");

        // В реальной реализации здесь должно быть обновление имени объекта

        await repository.UpdateAsync(obj, cancellationToken);

        logger.LogInformation("Object with ID {Id} updated", id);

        return mapper.Map<ObjectResponse>(obj);
    }

    public async Task<IEnumerable<ObjectResponse>> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Searching objects by name: {Name}", name);

        var spec = new ObjectsByNameSpec(name);
        var objects = await repository.ListAsync(spec, cancellationToken);

        return mapper.Map<IEnumerable<ObjectResponse>>(objects);
    }
}