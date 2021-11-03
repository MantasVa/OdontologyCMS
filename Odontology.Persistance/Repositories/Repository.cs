using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Odontology.Domain.Interfaces;
using Odontology.Persistance.Interfaces;

namespace Odontology.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext ApplicationDbContext;
        protected DbSet<TEntity> Entities;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
            Entities = applicationDbContext.Set<TEntity>();
        }
        public IQueryable<TEntity> GetAllQuery() => Query().AsNoTracking();

        public async Task<TEntity> GetByIdAsync(int id) 
            => await Query().Where(e => e.Id == id).AsNoTracking().FirstOrDefaultAsync();

        public TEntity Add(TEntity entity)
        {
            if(entity == null) throw new ArgumentNullException("Entity is null");
            Entities.Add(entity);
            ApplicationDbContext.SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if(entity == null) throw new ArgumentNullException("Entity is null");
            TEntity dbEntry = GetById(entity.Id);
            if (dbEntry == null) return entity;
            ApplicationDbContext.Entry(dbEntry).CurrentValues.SetValues(entity);
            await ApplicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            TEntity dbEntry = GetById(id);
            if (dbEntry == null) return dbEntry;
            Entities.Remove(dbEntry);
            await ApplicationDbContext.SaveChangesAsync();
            return dbEntry;
        }

        protected IQueryable<TEntity> Query()
        {
            var query = Entities.AsQueryable();
            foreach (var property in ApplicationDbContext.Model.FindEntityType(typeof(TEntity)).GetNavigations())
            {
                query = query.Include(property.Name);
            }
            query = query.OrderByDescending(x => x.CreatedOn);
            return query;
        }

        private TEntity GetById(int id) => Query().FirstOrDefault(e => e.Id == id);
    }
}
