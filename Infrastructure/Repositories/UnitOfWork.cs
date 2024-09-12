using Application.Repositories;
using Domain.Contracts;
using Infrastructure.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork<TId> : IUnitOfWork<TId>
    {
        private readonly ApplicationDbContext _context;
        private bool disposed;
        private Hashtable _respositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public IReadRepositoryAsync<T, TId> ReadRepositoryFor<T>() where T : BaseEntity<TId>
        {
            if (_respositories == null)
            {
                _respositories = new Hashtable();
            }

            var type = $"{typeof(T).Name}_Read";
            if (!_respositories.ContainsKey(type))
            {
                var repositoryType = typeof(ReadRepositoryAsync<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), _context);
                _respositories.Add(type, repositoryInstance);
            }

            return (IReadRepositoryAsync<T,TId>) _respositories[type];

        }

        public IWriteRepositoryAsync<T, TId> WriteRepositoryFor<T>() where T : BaseEntity<TId>
        {
            if (_respositories == null)
            {
                _respositories = new Hashtable();
            }

            var type = $"{typeof(T).Name}_Write";
            if (!_respositories.ContainsKey(type))
            {
                var repositoryType = typeof(WriteRepositoryAsync<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), _context);
                _respositories.Add(type, repositoryInstance);
            }

            return (IWriteRepositoryAsync<T, TId>)_respositories[type];
        }

        //public IAccountingRulesRepositoryAsync<T, TId> AccountingRulesRepositoryFor<T>() where T : BaseEntity<TId>
        //{
        //    if (_respositories == null)
        //    {
        //        _respositories = new Hashtable();
        //    }

        //    var type = $"{typeof(T).Name}_Write";
        //    if (!_respositories.ContainsKey(type))
        //    {
        //        var repositoryType = typeof(AccountingRulesRepositoryAsync<,>);
        //        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), _context);
        //        _respositories.Add(type, repositoryInstance);
        //    }

        //    return (IAccountingRulesRepositoryAsync<T, TId>)_respositories[type];
        //}

        public void Dispose()
        {
           Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
    }
}
