using Application.Repositories;
using Domain.Contracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class WriteRepositoryAsync<T, TId> : IWriteRepositoryAsync<T, TId> where T : BaseEntity<TId>
    {
        //private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        //public WriteRepositoryAsync(ApplicationDbContext context)
        public WriteRepositoryAsync(IDbContextFactory<ApplicationDbContext> contextFactory)  //special contextFactory dependency injection
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> AddAsync(T entity)
        {
            using var context = _contextFactory.CreateDbContext();  //here is where the context is created
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();  //save changes to the database
            return entity;
        } //context is disposed of after this method is done

        public Task DeleteAsync(T entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Set<T>().Remove(entity);
            context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            using var context = _contextFactory.CreateDbContext();
            T entityInDb = await context.Set<T>().FindAsync(entity.Id);
            context.Entry(entityInDb).CurrentValues.SetValues(entity);
            context.Entry(entityInDb).State = EntityState.Modified; //mark the entity as modified
            await context.SaveChangesAsync();  //save changes to the database
            return entity;
        }
    }
}
