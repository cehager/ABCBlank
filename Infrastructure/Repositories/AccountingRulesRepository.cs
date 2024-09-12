using Application.Repositories;
using Domain.Contracts;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AccountingRulesRepositoryAsync<T, TId> : IAccountingRulesRepositoryAsync<T, TId> where T : BaseEntity<TId>
    {
        private readonly ApplicationDbContext _context;

        public AccountingRulesRepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<T> UpdateAccountRecordsAsync(T entity)
        {
            //find the related accounting records - transactions
            T entityInDb = await _context.Set<T>().FindAsync(entity.Id);
            _context.Entry(entityInDb).CurrentValues.SetValues(entity);
            return entity;
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
