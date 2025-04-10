using Application.Repositories;
using Domain.Contracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork<TId> : IUnitOfWork<TId>
    {
        //private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private bool disposed;
        private Hashtable _respositories;

        //public UnitOfWork(ApplicationDbContext context)  //dependency injection
        public UnitOfWork(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken)  //TODO: remove this method
        {
            await Task.CompletedTask;

            return -1; // await _context.SaveChangesAsync(cancellationToken);
        }

        public IReadRepositoryAsync<T, TId> ReadRepositoryFor<T>() where T : BaseEntity<TId>
        {
            try
            {
                if (_respositories == null)
                {
                    _respositories = new Hashtable();
                }

                var type = $"{typeof(T).Name}_Read";
                if (!_respositories.ContainsKey(type))
                {
                    var repositoryType = typeof(ReadRepositoryAsync<,>);
                    var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), _contextFactory);
                    _respositories.Add(type, repositoryInstance);
                }

                return (IReadRepositoryAsync<T, TId>)_respositories[type];
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating repository instance: {ex.Message}", ex);
            }
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
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), _contextFactory);
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

        //public void Dispose()  //TODO: remove this method
        //{
        //   Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            //_context.Dispose();
        //        }
        //    }
        //    disposed = true;
        //}
    }
}
