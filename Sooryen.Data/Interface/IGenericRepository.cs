using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sooryen.Data.Interface
{
    public interface IGenericRepository<T>
    {

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        ICollection<T> GetList(params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        ICollection<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<T>> GetListAsync();

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Gets the single asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns></returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// Finds the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> match);

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        Task<T> FindAsync(Expression<Func<T, bool>> match);

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        ICollection<T> FindAll(Expression<Func<T, bool>> match);

        /// <summary>
        /// Finds all asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);

        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <param name="items">The items.</param>
        void AddEntity(params T[] items);

        /// <summary>
        /// Adds the entity asynchronous.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        Task AddEntityAsync(params T[] items);

        /// <summary>
        /// Adds the related entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="addSet">The add set.</param>
        void AddRelatedEntity(T item, IEnumerable<object> addSet);

        /// <summary>
        /// Adds the related entity asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="addSet">The add set.</param>
        /// <returns></returns>
        Task AddRelatedEntityAsync(T item, IEnumerable<object> addSet);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="items">The items.</param>
        void UpdateEntity(params T[] items);

        /// <summary>
        /// Updates the related entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="updatedSet">The updated set.</param>
        void UpdateRelatedEntity(T item, IEnumerable<object> updatedSet);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="items">The items.</param>
        Task UpdateEntityAsync(params T[] items);

        /// <summary>
        /// Updates the related entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="updatedSet">The updated set.</param>
        Task UpdateRelatedEntityAsync(T item, IEnumerable<object> updatedSet);

        /// <summary>
        /// Removes the entity.
        /// </summary>
        /// <param name="items">The items.</param>
        void RemoveEntity(params T[] items);

        /// <summary>
        /// Removes the related entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="updatedSet">The updated set.</param>
        void RemoveRelatedEntity(T item, IEnumerable<object> updatedSet);

        /// <summary>
        /// Removes the entity.
        /// </summary>
        /// <param name="items">The items.</param>
        Task RemoveEntityAsync(params T[] items);

        /// <summary>
        /// Removes the related entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="updatedSet">The updated set.</param>
        Task RemoveRelatedEntityAsync(T item, IEnumerable<object> updatedSet);


    }
}
