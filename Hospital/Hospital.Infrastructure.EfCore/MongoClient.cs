using System.Collections.Immutable;
using Hospital.Domain;
using MongoDB.Driver;

namespace Hospital.Infrastructure.EfCore;
public class MongoClient<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    protected readonly IMongoCollection<TEntity> Collection;

    protected MongoClient(IMongoDatabase database, string collectionName)
    {
        Collection = database.GetCollection<TEntity>(collectionName);
    }

    public async Task<TEntity> Create(TEntity entity)
    {
        await Collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<List<TEntity>> ReadAll()
    {
        return await Collection.Find(_ => true).ToListAsync();
    }

    public async Task<TEntity?> Read(TKey id)
    {
        var filter = Builders<TEntity>.Filter.Eq("Id", id);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<TEntity?> Update(TKey id, TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq("Id", id);
        await Collection.ReplaceOneAsync(filter, entity);
        return entity;
    }

    public async Task<bool> Delete(TKey id)
    {
        var filter = Builders<TEntity>.Filter.Eq("Id", id);
        var result = await Collection.DeleteOneAsync(filter);
        return result.DeletedCount == 1;
    }
    
    private static TKey GetEntityId(TEntity entity)
    {
        var idProperty = typeof(TEntity).GetProperty("Id")
                         ?? throw new InvalidOperationException($"Entity {typeof(TEntity).Name} must have an 'Id' property.");

        var value = idProperty.GetValue(entity);
        if (value == null) throw new InvalidOperationException("Id cannot be null.");

        return (TKey)value;
    }
}