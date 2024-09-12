using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IAccountingRulesRepositoryAsync<T, in TId> where T : class, IEntity<TId>
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);

        Task<T> UpdateAccountRecordsAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
