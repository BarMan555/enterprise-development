namespace Hospital.Infrastructure.InMemory.Seeders;

public interface ISeeder<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    public List<TEntity> GetItems();

    public TKey GetCurrentId();
}