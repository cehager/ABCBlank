using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IReadRepositoryAsync<T, in TId> where T : class, IEntity<TId>
    {
        Task<T> GetByIdAsync(TId id);
        Task<List<T>> GetAllAsync();
        IQueryable<T> Entities {  get; }

    }
}
