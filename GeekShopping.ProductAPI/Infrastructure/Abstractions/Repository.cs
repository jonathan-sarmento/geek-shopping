using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GeekShopping.ProductAPI.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Infrastructure.Abstractions
{
    public abstract class Repository<TDomainModel, TModel, TId> : IRepository<TDomainModel, TModel, TId>
        where TModel : SimpleId<TId>
        where TDomainModel : SimpleId<TId>
    {
        private readonly DbContext _context;
        private readonly DbSet<TModel> _dbSet;
        private readonly IMapper _mapper;

        protected Repository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<TModel>();
        }

        public virtual async Task DeleteAsync(TId id, CancellationToken cancellationToken)
        {
            _dbSet.Remove(await FindByIdAsync(id, cancellationToken).ConfigureAwait(false));
            await _context.SaveChangesAsync(true, cancellationToken);
        }
        
        public virtual async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken) =>
            await _dbSet.AsNoTracking().AnyAsync(x => Equals(x.Id, id), cancellationToken);

        public virtual async Task AddAsync(TDomainModel domainModel, CancellationToken cancellationToken)
        {
            if (await ExistsAsync(domainModel.Id, cancellationToken).ConfigureAwait(false)) return;

            var model = MapToModel(domainModel);
            await _dbSet.AddAsync(model, cancellationToken);
            await _context.SaveChangesAsync(true, cancellationToken);

            MapToDomain(model, domainModel);
        }
        
        public virtual async Task<TDomainModel> SelectByIdAsync(TId id, CancellationToken cancellationToken)
        {
            return MapToDomain(await _dbSet.FindAsync(new object[] {id}, cancellationToken));
        }
        
        private async Task<TModel> FindByIdAsync(TId id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(new object[] {id}, cancellationToken);
        }

        public async Task<IEnumerable<TDomainModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<TDomainModel>>(await _dbSet.ToListAsync());
        }

        public async Task UpdateAsync(TDomainModel domainModel, CancellationToken cancellationToken)
        {
            if (!(await ExistsAsync(domainModel.Id, cancellationToken).ConfigureAwait(false))) return;

            var model = MapToModel(domainModel);
            _dbSet.Update(model);
            await _context.SaveChangesAsync(cancellationToken);
        }
        private TDomainModel MapToDomain(TModel model, TDomainModel domain)
            => _mapper.Map(model, domain);
        
        private TDomainModel MapToDomain(TModel model)
            => _mapper.Map<TModel, TDomainModel>(model);
        
        private TModel MapToModel(TDomainModel domain)
            => _mapper.Map<TDomainModel, TModel>(domain);
    
    }
}