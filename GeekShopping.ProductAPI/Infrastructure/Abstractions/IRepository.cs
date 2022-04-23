using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GeekShopping.ProductAPI.Domain.Base;

namespace GeekShopping.ProductAPI.Infrastructure.Abstractions
{
    public interface IRepository<TEntity, in TId>
        where TEntity : SimpleId<TId>
    {
        void Delete(TId id);
        Task DeleteAsync(TId id, CancellationToken cancellationToken);

        bool Exists(TId id);
        Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken);

        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken);

        IQueryable<TEntity> SelectAll();

        TEntity SelectById(TId id);
        Task<TEntity> SelectByIdAsync(TId id, CancellationToken cancellationToken);

        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity, CancellationToken cancelletionToken);
    }
}