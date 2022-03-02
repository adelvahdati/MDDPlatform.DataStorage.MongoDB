using MDDPlatform.DataStorage.Core;
using MDDPlatform.SharedKernel.Entities;

namespace MDDPlatform.DataStorage.MongoDB.Repositories
{
    public interface IMongoRepository<T, TId> : IRepository<T,TId> where T : BaseEntity<TId>
    {
        
    }
}