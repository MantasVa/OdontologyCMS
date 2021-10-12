﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Odontology.Domain.Interfaces;
using Odontology.Persistance.Interfaces;

namespace Odontology.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext applicationDbContext;
        protected DbSet<TEntity> entities;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            entities = applicationDbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id) 
            => await Query().Where(e => e.Id == id).AsNoTracking().FirstOrDefaultAsync();


        public IQueryable<TEntity> GetAllQuery() => Query().AsNoTracking();

        public TEntity Add(TEntity entity)
        {
            if(entity == null) throw new ArgumentNullException("Entity is null");
            entities.Add(entity);
            applicationDbContext.SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if(entity == null) throw new ArgumentNullException("Entity is null");
            TEntity dbEntry = GetById(entity.Id);
            if (dbEntry == null) return entity;
            applicationDbContext.Entry(dbEntry).CurrentValues.SetValues(entity);
            applicationDbContext.SaveChanges();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            TEntity dbEntry = GetById(id);
            if (dbEntry == null) return dbEntry;
            entities.Remove(dbEntry);
            applicationDbContext.SaveChanges();
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

        private TEntity GetById(int id) => Query().FirstOrDefault(e => e.Id == id);
    }
}