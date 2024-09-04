using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IUnitOfWork<TId> : IDisposable
    {
        IWriteRepositoryAsync<T, TId> WriteRepositoryFor<T>() where T : BaseEntity<TId>;
        IReadRepositoryAsync<T, TId> ReadRepositoryFor<T>() where T : BaseEntity<TId>;
        Task<int> CommitAsync(CancellationToken cancellation);
    }
}
