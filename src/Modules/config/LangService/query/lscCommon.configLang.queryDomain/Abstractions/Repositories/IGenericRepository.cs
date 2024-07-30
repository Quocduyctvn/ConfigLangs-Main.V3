using lscCommon.configLang.queryContract.DependencyInjection.Options;
using lscCommon.configLang.queryDomain.Abstractions.Entities;
using System.Linq.Expressions;

namespace lscCommon.configLang.queryDomain.Abstractions.Repositories
{
    /// <summary>
    /// Provide generic repository with custom type of key
    /// </summary>
    /// <typeparam name="TEntity">Generic type of domain entity</typeparam>
    /// <typeparam name="TKey">Generic type of entity key</typeparam>
    public interface IGenericRepository<TEntity, in TKey>
        where TEntity : IEntity
    {
        /// <summary>
        /// Find domain entity by id. Returned entity can be tracking
        /// </summary>
        /// <param name="id">ID of domain entity</param>
        /// <param name="isTracking">Tracking state of entity</param>
        /// <param name="allowNullReturn">Allow null return. If false, when value is null will throw not found exception</param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>Domain entity with given id or null if entity with given id not found</returns>
        Task<TEntity?> FindByIdAsync(TKey id,
                                     bool isTracking = false,
                                     bool allowNullReturn = true,
                                     CancellationToken cancellationToken = default,
                                     params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Find entity by id. Returned entity can be tracking
        /// </summary>
        /// <param name="id">ID of domain entity</param>
        /// <param name="option">Option to find entity</param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>Domain entity with given id or null if entity with given id not found</returns>
        Task<TEntity?> FindByIdAsync(TKey id,
                                     FindOption option,
                                     CancellationToken cancellationToken = default,
                                     params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Find single entity that satisfied predicate expression. Can be tracking
        /// </summary>
        /// <param name="isTracking">Tracking state of entity</param>
        /// <param name="allowNullReturn">Allow null return. If false, when value is null will throw not found exception</param>
        /// <param name="predicate">Predicate expression</param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>Domain entity matched expression or null if entity not found</returns>
        Task<TEntity?> FindSingleAsync(bool isTracking = false,
                                       bool allowNullReturn = true,
                                       Expression<Func<TEntity, bool>>? predicate = null,
                                       CancellationToken cancellationToken = default,
                                       params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Find all entity that satisfied predicate expression. Can be tracking
        /// </summary>
        /// <param name="isTracking">Tracking state of entity</param>
        /// <param name="predicate">Predicate expression</param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>IQueryable of entities that match predicate expression</returns>
        IQueryable<TEntity> FindAll(bool isTracking = false,
                                    Expression<Func<TEntity, bool>>? predicate = null,
                                    params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
