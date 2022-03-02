using System.Linq.Expressions;
using MDDPlatform.SharedKernel.Entities;
using MongoDB.Driver;

namespace MDDPlatform.DataStorage.MongoDB.Repositories
{
    public class MongoRepository<T, TId> : IMongoRepository<T, TId> where T : BaseEntity<TId>
    {
        protected IMongoCollection<T> Collection { get; }
        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
            => await Collection.Find(predicate).SingleOrDefaultAsync();

        public async Task<T> GetAsync(TId id)
            => await Collection.Find(e => e.Id.Equals(id)).SingleOrDefaultAsync();
        

        public async Task<IReadOnlyList<T>> ListAsync()
            => await Collection.Find<T>(FilterDefinition<T>.Empty).ToListAsync();

        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate)
            => await Collection.Find(predicate).ToListAsync();

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
            => await Collection.Find(predicate).AnyAsync();

        public async Task<long> CountAsync(Expression<Func<T, bool>> predicate)
            => await Collection.CountDocumentsAsync(predicate);
            
        public async Task AddAsync(T entity) =>
            await Collection.InsertOneAsync(entity);

        public async Task UpdateAsync(T entity)
            => await Collection.ReplaceOneAsync(e => e.Id.Equals(entity.Id), entity);
        
        public async Task DeleteAync(TId Id) =>
            await Collection.DeleteOneAsync(e=>e.Id.Equals(Id));
        public async Task DeleteAsync(T entity) =>
            await Collection.DeleteOneAsync(e => e.Id.Equals(entity.Id));
    }
}