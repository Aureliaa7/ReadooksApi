﻿using Microsoft.EntityFrameworkCore;
using Readooks.DataAccessLayer.DatabaseContext;
using Readooks.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Readooks.DataAccessLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext Context;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<T> Add(T entity)
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Get(Guid id)
        {
            var searched = await Context.Set<T>().FindAsync(id);
            return searched;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> Remove(Guid id)
        {
            var entityToBeDeleted = await Context.Set<T>().FindAsync(id);
            if (entityToBeDeleted == null)
            {
                return entityToBeDeleted;
            }
            Context.Set<T>().Remove(entityToBeDeleted);
            await Context.SaveChangesAsync();
            return entityToBeDeleted;
        }


        public async Task<T> Update(T entity)
        {
            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            var exists = Context.Set<T>().Where(predicate);
            return Task.FromResult(exists.Any());
        }
    }
}
