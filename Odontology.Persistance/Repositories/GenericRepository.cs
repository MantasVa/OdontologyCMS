using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Odontology.Domain.Interfaces;
using Odontology.Persistance.Interfaces;

namespace Odontology.Persistance.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext applicationDbContext;
        protected DbSet<TEntity> entities;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            entities = applicationDbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id) 
            => await Query().Where(e => e.Id == id).FirstOrDefaultAsync();


        public IQueryable<TEntity> GetAllQuery() => Query();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entities.Add(entity);
            await applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            TEntity dbEntry = await GetByIdAsync(entity.Id);
            if (dbEntry == null) return entity;
            applicationDbContext.Entry(dbEntry).CurrentValues.SetValues(entity);
            await applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            TEntity dbEntry = await GetByIdAsync(id);
            if (dbEntry == null) return dbEntry;
            entities.Remove(dbEntry);
            await applicationDbContext.SaveChangesAsync();
            return dbEntry;
        }

        protected IQueryable<TEntity> Query()
        {
            var query = entities.AsQueryable();
            foreach (var property in applicationDbContext.Model.FindEntityType(typeof(TEntity)).GetNavigations())
            {
                query = query.Include(property.Name);
            }
            query = query.OrderByDescending(x => x.CreatedOn);
            return query;
        }
    }
}
