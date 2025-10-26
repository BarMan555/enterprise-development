namespace Hospital.Domain.Seeders;

/// <summary>
/// Interface for any seeder
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface ISeeder<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Get list of data
    /// </summary>
    /// <returns>List of data</returns>
    public List<TEntity> GetItems();

    /// <summary>
    /// Get ID to start counting
    /// </summary>
    /// <returns>ID</returns>
    public TKey GetCurrentId();
}