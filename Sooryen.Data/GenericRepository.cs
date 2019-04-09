using Sooryen.Data.Interface;
using Sooryen.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Sooryen.Data
{
    /// <summary>
    /// GenericRepository class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        /// <summary>
        /// The administration context
        /// </summary>
        protected TempSooryenDemoContext context { get; set; }

        /// <summary>
        /// The database set
        /// </summary>
        internal DbSet<T> dbSet;


        public GenericRepository()
        {
            context = new TempSooryenDemoContext();
            dbSet = context.Set<T>();
        }

        /// <summary>
        /// The _disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = context.Set<T>();
            //Apply eager loading
            if (navigationProperties != null)
            {
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include(navigationProperty);
                }
            }
            return dbQuery;

        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public ICollection<T> GetList(params Expression<Func<T, object>>[] navigationProperties)
        {
            var dbQuery = context.Set<T>().AsQueryable();
            if (navigationProperties != null)
            {
                dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
            }

            return dbQuery.ToList();

        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public ICollection<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            var dbQuery = context.Set<T>().AsQueryable();
            if (navigationProperties != null)
            {
                dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
            }
            var list = dbQuery.Where(@where).AsNoTracking().AsQueryable();
            return list.ToList();

        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<T>> GetListAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public virtual T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = context.Set<T>();
            //Apply eager loading
            if (navigationProperties != null)
            {
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include(navigationProperty);
                }
            }
            //Don't track any changes for the selected item
            var item = dbQuery.AsNoTracking().FirstOrDefault(@where);
            return item;
        }

        /// <summary>
        /// Gets the single asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = context.Set<T>();
            //Apply eager loading
            if (navigationProperties != null)
            {
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include(navigationProperty);
                }
            }
            //Don't track any changes for the selected item
            var item = await dbQuery.AsNoTracking().FirstOrDefaultAsync(@where);
            return item;
        }

        /// <summary>
        /// Finds the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public T Find(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().SingleOrDefault(match);
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().SingleOrDefaultAsync(match);
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().Where(match).ToList();
        }

        /// <summary>
        /// Finds all asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().Where(match).ToListAsync();
        }

        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <param name="items">The items.</param>
        public virtual void AddEntity(params T[] items)
        {
            using (var dbContext = new TempSooryenDemoContext())
            {
                if (items != null)
                {
                    foreach (T item in items)
                    {

                        dbContext.Entry(item).State = EntityState.Added;
                    }
                }
                dbContext.SaveChanges();
            }

        }

        /// <summary>
        /// Adds the entity asynchronous.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public virtual async Task AddEntityAsync(params T[] items)
        {
            try
            {
                if (items != null)
                {
                    foreach (T item in items)
                    {
                        context.Entry(item).State = EntityState.Added;
                    }
                }
                await context.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                .SelectMany(x => x.ValidationErrors)
                .Select(x => x.ErrorMessage);

               
            }
            catch (Exception ex)
            {
// ignore
            }
        }

        /// <summary>
        /// Adds the related entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="addSet">The add set.</param>
        public virtual void AddRelatedEntity(T item, IEnumerable<object> addSet)
        {
            //using (var scope = new  TransactionScope())
            //{
            context.Entry(item).State = EntityState.Added;
            foreach (var entity in addSet.ToList())
            {
                context.Entry(entity).State = EntityState.Added;
            }
            context.SaveChanges();
            //    scope.Complete();
            //}
        }

        /// <summary>
        /// Adds the related entity asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="addSet">The add set.</param>
        /// <returns></returns>
        public async Task AddRelatedEntityAsync(T item, IEnumerable<object> addSet)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                context.Entry(item).State = EntityState.Added;
                foreach (var entity in addSet.ToList())
                {
                    context.Entry(entity).State = EntityState.Added;
                }
                await context.SaveChangesAsync().ConfigureAwait(false);
                scope.Complete();
            }
        }


        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="items">The items.</param>
        public virtual void UpdateEntity(params T[] items)
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Modified;
                }
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Updates the related entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="updatedSet">The updated set.</param>
        public virtual void UpdateRelatedEntity(T item, IEnumerable<object> updatedSet)
        {
            using (var scope = new TransactionScope())
            {
                context.Entry(item).State = EntityState.Modified;
                foreach (var entity in updatedSet.ToList())
                {
                    context.Entry(entity).State = EntityState.Modified;
                }
                context.SaveChanges();
                scope.Complete();
            }
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="items">The items.</param>
        public virtual async Task UpdateEntityAsync(params T[] items)
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Modified;
                }
            }
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the related entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="updatedSet">The updated set.</param>
        public virtual async Task UpdateRelatedEntityAsync(T item, IEnumerable<object> updatedSet)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                context.Entry(item).State = EntityState.Modified;
                foreach (var entity in updatedSet.ToList())
                {
                    context.Entry(entity).State = EntityState.Modified;
                }
                await context.SaveChangesAsync().ConfigureAwait(false);
                scope.Complete();
            }
        }

        /// <summary>
        /// Removes the entity.
        /// </summary>
        /// <param name="items">The items.</param>
        public virtual void RemoveEntity(params T[] items)
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Removes the related entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="updatedSet">The updated set.</param>
        public virtual void RemoveRelatedEntity(T item, IEnumerable<object> updatedSet)
        {
            using (var scope = new TransactionScope())
            {
                foreach (var entity in updatedSet.ToList())
                {
                    context.Entry(entity).State = EntityState.Deleted;
                }
                context.Entry(item).State = EntityState.Deleted;
                context.SaveChanges();
                scope.Complete();
            }
        }

        /// <summary>
        /// Removes the entity.
        /// </summary>
        /// <param name="items">The items.</param>
        public virtual async Task RemoveEntityAsync(params T[] items)
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }
            }
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes the related entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="updatedSet">The updated set.</param>
        public virtual async Task RemoveRelatedEntityAsync(T item, IEnumerable<object> updatedSet)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (var entity in updatedSet.ToList())
                {
                    context.Entry(entity).State = EntityState.Deleted;
                }
                context.Entry(item).State = EntityState.Deleted;
                await context.SaveChangesAsync().ConfigureAwait(false);
                scope.Complete();
            }

        }

     

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
