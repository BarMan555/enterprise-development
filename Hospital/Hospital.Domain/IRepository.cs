namespace Hospital.Domain;

/// <summary>
/// Interface for repositiry entities
/// </summary>
/// <typeparam name="TEntity">Type of entity</typeparam>
/// <typeparam name="TKey">Type of id entity</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Create new object
    /// </summary>
    /// <param name="entity">Entity for creating</param>
    /// <returns>ID of new object</returns>
    public TKey Create(TEntity entity);
    
    /// <summary>
    /// Get list of all entities from repository
    /// </summary>
    /// <returns>List of all objects</returns>
    public List<TEntity> ReadAll();
    
    /// <summary>
    /// Get entity from repository by its ID
    /// </summary>
    /// <param name="entityId">ID of entity</param>
    /// <returns>Object</returns>
    public TEntity? Read(TKey entityId);
    
    /// <summary>
    /// Update information about entity by its ID
    /// </summary>
    /// <param name="entityId">ID of old entity</param>
    /// <param name="entity">New entity</param>
    /// <returns></returns>
    public TEntity? Update(TKey entityId, TEntity entity);
    
    /// <summary>
    /// Delete entity from repository
    /// </summary>
    /// <param name="entityId">ID of entity</param>
    /// <returns>Result of deleting</returns>
    public bool Delete(TKey entityId);
}