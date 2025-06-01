using System.Linq.Expressions;
using MDDPlatform.DataStorage.Core;
using MDDPlatform.SharedKernel.Entities;
using MongoDB.Driver.Linq;

namespace MDDPlatform.DataStorage.MongoDB.Repositories
{
    public interface IMongoRepository<T, TId> : IRepository<T,TId> where T : BaseEntity<TId>
    {
        IQueryable<T> GetQueryableCollection();
        Task<IReadOnlyList<T>> ListAsync(List<TId> Ids);
        Task DeleteAsync(List<TId> Ids);
        Task<bool> DeleteAsync(Expression<Func<T,bool>> predicate);
        Task InsertOrReplaceAsync(T entity);
    }
}