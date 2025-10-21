using Hospital.Domain;
using Hospital.Infrastructure.InMemory.Seeders;

namespace Hospital.Infrastructure.InMemory.Repositories;

public abstract class InMemoryRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    
    private readonly List<TEntity> _entities;

    protected TKey CurrentId;

    protected InMemoryRepository(ISeeder<TEntity, TKey> seeder)
    {
        _entities = seeder.GetItems();
        CurrentId = seeder.GetCurrentId();
    }

    public virtual TKey Create(TEntity entity)
    {
        var entityId = GenerateId();
        SetEntityId(entity, entityId);
        _entities.Add(entity);
        return entityId;
    }

    public virtual List<TEntity> ReadAll() => _entities;

    public virtual TEntity? Read(TKey entityId) => _entities.FirstOrDefault(x => GetEntityId(x).Equals(entityId));

    public virtual TEntity? Update(TKey entityId, TEntity newEntity)
    {
        var oldEntity = Read(entityId);
        if (oldEntity == null) return null;
        
        oldEntity = newEntity;
        SetEntityId(newEntity, entityId);
        
        return newEntity;
    }

    public virtual bool Delete(TKey entityId)
    {
        var entity = Read(entityId);
        if (entity == null) return false;
        
        _entities.Remove(entity);
        return true;
    }
    
    protected abstract TKey GetEntityId(TEntity entity);
    
    protected abstract void SetEntityId(TEntity entity, TKey id);

    protected abstract TKey GenerateId();
}