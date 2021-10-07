using Microsoft.EntityFrameworkCore.Storage;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Forum.Challenger.Domain.Contracts.Repositories.EntityFramework;

namespace Forum.Challenger.Infra.Repositories.EntityFramework
{
    public abstract class BaseEFRepository<T> : IBaseEFRepository<T> where T : class
    {
        public DbSet<T> dbSet;

        protected readonly DbContext dbContext;

        protected BaseEFRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;

            dbSet = this.dbContext?.Set<T>();
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            dbContext.Entry<T>(entity).State = EntityState.Modified;

            dbSet.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            dbSet.UpdateRange(entities);
        }

        public virtual IEnumerable<T> List(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return dbSet;

            return dbSet.Where(predicate);
        }

        public virtual async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await dbSet.ToListAsync();

            return await dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<List<T>> ListNoTrackingAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await dbSet.AsNoTracking().ToListAsync();

            return await dbSet.Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual IEnumerable<T> ListNoTracking(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return dbSet.AsNoTracking();

            return dbSet.AsNoTracking().Where(predicate);
        }

        public virtual async Task<(int count, IEnumerable<T>)> PaggingAsync(Expression<Func<T, bool>> predicate = null, int page = 0, int elements = 15)
        {
            var listItems = dbSet.Where(predicate);

            if (!listItems.Any())
                return (0, null);

            return (listItems.Count(), await listItems.Skip(page * elements).Take(elements).ToListAsync());
        }

        public virtual T First(Expression<Func<T, bool>> predicate = null)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        public virtual T FirstNoTracking(Expression<Func<T, bool>> predicate = null)
        {
            return dbSet.AsNoTracking().FirstOrDefault(predicate);
        }

        public virtual int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public virtual IDbContextTransaction BeginTransaction()
        {
            return dbContext.Database.BeginTransaction();
        }

        public virtual async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await dbContext.Database.BeginTransactionAsync();
        }

        public virtual void RollbackTransaction()
        {
            dbContext.Database.RollbackTransaction();
        }

        public async Task RollbackTransactionAsync()
        {
            dbContext.Database.RollbackTransactionAsync();
        }

        public virtual void CommitTransaction()
        {
            dbContext.Database.CommitTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            dbContext.Database.CommitTransactionAsync();
        }
    }
}
