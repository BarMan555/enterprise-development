using Hospital.Domain;
using Hospital.Infrastructure.InMemory.Seeders;

namespace Hospital.Infrastructure.InMemory.Repositories;

/// <summary>
/// Abstract class for any InMemory repository
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class InMemoryRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// List of entities in repositury
    /// </summary>
    private readonly List<TEntity> _entities;

    /// <summary>
    /// ID counter
    /// </summary>
    protected TKey CurrentId;

    /// <summary>
    /// Constructor for making repository
    /// </summary>
    /// <param name="seeder">Data for repository</param>
    protected InMemoryRepository(ISeeder<TEntity, TKey> seeder)
    {
        _entities = seeder.GetItems();
        CurrentId = seeder.GetCurrentId();
    }

    /// <summary>
    /// Create new object
    /// </summary>
    /// <param name="entity">Entity for creating</param>
    /// <returns>ID of new object</returns>
    public virtual TKey Create(TEntity entity)
    {
        var entityId = GenerateId();
        SetEntityId(entity, entityId);
        _entities.Add(entity);
        return entityId;
    }

    /// <summary>
    /// Get list of all entities from repository
    /// </summary>
    /// <returns>List of all objects</returns>
    public virtual List<TEntity> ReadAll() => _entities;

    /// <summary>
    /// Get entity from repository by its ID
    /// </summary>
    /// <param name="entityId">ID of entity</param>
    /// <returns>Object</returns>
    public virtual TEntity? Read(TKey entityId) => _entities.FirstOrDefault(x => GetEntityId(x).Equals(entityId));

    /// <summary>
    /// Update information about entity by its ID
    /// </summary>
    /// <param name="entityId">ID of old entity</param>
    /// <param name="entity">New entity</param>
    /// <returns></returns>
    public virtual TEntity? Update(TKey entityId, TEntity newEntity)
    {
        var oldEntity = Read(entityId);
        if (oldEntity == null) return null;
        
        oldEntity = newEntity;
        SetEntityId(newEntity, entityId);
        
        return newEntity;
    }

    /// <summary>
    /// Delete entity from repository
    /// </summary>
    /// <param name="entityId">ID of entity</param>
    /// <returns>Result of deleting</returns>
    public virtual bool Delete(TKey entityId)
    {
        var entity = Read(entityId);
        if (entity == null) return false;
        
        _entities.Remove(entity);
        return true;
    }
    
    /// <summary>
    /// Get ID of input entity
    /// </summary>
    /// <param name="entity">entity</param>
    /// <returns>ID of entity</returns>
    protected abstract TKey GetEntityId(TEntity entity);
    
    /// <summary>
    /// Set ID to entity
    /// </summary>
    /// <param name="entity">entity</param>
    /// <param name="id">ID to entity</param>
    protected abstract void SetEntityId(TEntity entity, TKey id);

    /// <summary>
    /// Generate new ID
    /// </summary>
    /// <returns>New ID</returns>
    protected abstract TKey GenerateId();
}