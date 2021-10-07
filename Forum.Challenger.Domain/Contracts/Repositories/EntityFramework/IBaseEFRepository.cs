using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Forum.Challenger.Domain.Contracts.Repositories.EntityFramework
{
    public interface IBaseEFRepository<T> where T : class
    {
        void Add(T entity);

        Task AddAsync(T entity);

        void Remove(T entity);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        IEnumerable<T> List(Expression<Func<T, bool>> predicate = null);

        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate = null);

        Task<List<T>> ListNoTrackingAsync(Expression<Func<T, bool>> predicate = null);

        IEnumerable<T> ListNoTracking(Expression<Func<T, bool>> predicate = null);

        Task<(int count, IEnumerable<T>)> PaggingAsync(Expression<Func<T, bool>> predicate = null, int page = 0, int elements = 15);

        T First(Expression<Func<T, bool>> predicate = null);

        T FirstNoTracking(Expression<Func<T, bool>> predicate = null);

        int SaveChanges();

        Task<int> SaveChangesAsync();

        IDbContextTransaction BeginTransaction();

        Task<IDbContextTransaction> BeginTransactionAsync();

        void RollbackTransaction();

        Task RollbackTransactionAsync();

        void CommitTransaction();

        Task CommitTransactionAsync();
    }
}
