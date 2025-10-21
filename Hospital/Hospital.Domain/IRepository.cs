namespace Hospital.Domain;

public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    public TKey Create(TEntity entity);
    public List<TEntity> ReadAll();
    public TEntity? Read(TKey entityId);
    public TEntity? Update(TKey entityId, TEntity entity);
    public bool Delete(TKey entityId);
}