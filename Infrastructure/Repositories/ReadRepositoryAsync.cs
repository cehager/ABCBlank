using Application.Repositories;
using Domain.Contracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReadRepositoryAsync<T, TId> : IReadRepositoryAsync<T, TId> where T : BaseEntity<TId>
    {
        //private readonly ApplicationDbContext _context;
        private IDbContextFactory<ApplicationDbContext> _contextFactory;
       //public ReadRepositoryAsync(ApplicationDbContext context)
        public ReadRepositoryAsync(IDbContextFactory<ApplicationDbContext> contextFactory)  //dependency injection but passed in from the UnitOfWork
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<T>> GetAllAsync()
        {
           using var context = _contextFactory.CreateDbContext();  //here is where the context is created
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            using var context = _contextFactory.CreateDbContext();  //here is where the context is created
            return await context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> Entities
        {
            get
            {
                var context = _contextFactory.CreateDbContext();
                return context.Set<T>().AsQueryable();
            }
        } //this is a property that returns an IQueryable of T



    }
}
