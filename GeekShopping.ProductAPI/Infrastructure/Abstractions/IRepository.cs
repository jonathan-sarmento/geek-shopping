using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GeekShopping.ProductAPI.Domain.Base;

namespace GeekShopping.ProductAPI.Infrastructure.Abstractions
{
    public interface IRepository<TDomainModel, TModel, in TId>
        where TDomainModel : SimpleId<TId>
        where TModel : SimpleId<TId>
    {
        Task DeleteAsync(TId id, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken);
        Task AddAsync(TDomainModel domainModel, CancellationToken cancellationToken);
        Task<IEnumerable<TDomainModel>> GetAllAsync();
        Task<TDomainModel> SelectByIdAsync(TId id, CancellationToken cancellationToken);
        Task UpdateAsync(TDomainModel domainModel, CancellationToken cancellationToken);
    }
}