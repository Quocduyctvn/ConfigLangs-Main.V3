using lscCommon.configLang.queryContract.DependencyInjection.Options;
using lscCommon.configLang.queryContract.Exceptions;
using lscCommon.configLang.queryContract.Shared;
using lscCommon.configLang.queryDomain.Abstractions.Entities;
using lscCommon.configLang.queryDomain.Abstractions.Repositories;
using lscCommon.configLang.queryPersistence.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace lscCommon.configLang.queryPersistence.Repositories
{
    /// <summary>
    /// Implementation of IGenericRepository
    /// </summary>
    /// <typeparam name="TEntity">Generic type of domain entity</typeparam>
    /// <typeparam name="TKey">Generic type of entity key</typeparam>
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        private readonly ApplicationDbContext context;
        private DbSet<TEntity>? entities;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get entity DbSet
        /// </summary>
        protected DbSet<TEntity> Entities
        {
            get
            {
                if (entities == null) entities = context.Set<TEntity>();
                return entities;
            }
        }

        /// <summary>
        /// Find domain entity by id. Returned entity can be tracking
        /// </summary>
        /// <param name="id">ID of domain entity</param>
        /// <param name="allowNullReturn">Allow null return. If false, when value is null will throw not found exception</param>
        /// <param name="isTracking">Tracking state of entity</param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>Domain entity with given id or null if entity with given id not found</returns>
        public async Task<TEntity?> FindByIdAsync(TKey id,
                                                  bool allowNullReturn = false,
                                                  bool isTracking = false,
                                                  CancellationToken cancellationToken = default,
                                                  params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Initialize query from the entity set
            var query = Entities.AsQueryable();
            if (includeProperties.Any())
                // Include specified properties
                query.IncludeMultiple(includeProperties);

            // Apply tracking option
            query = isTracking ? query : query.AsNoTracking();
            // Find entity by Id
            var result = await query.FirstOrDefaultAsync(x => x.Id!.Equals(id), cancellationToken);
            if (result is null && !allowNullReturn)
                // Throw not found exception when result is null and don't allow null return
                throw new NotFoundException(MessConst.NOT_FOUND.FillArgs(new List<MessageArgs>
                {
                    new(Args.TABLE_NAME, typeof(TEntity).Name)
                }));
            return result;
        }

        /// <summary>
        /// Find entity by id. Returned entity can be tracking
        /// </summary>
        /// <param name="id">ID of domain entity</param>
        /// <param name="option">Option to find entity</param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>Domain entity with given id or null if entity with given id not found</returns>
        public Task<TEntity?> FindByIdAsync(TKey id,
                                            FindOption option,
                                            CancellationToken cancellationToken = default,
                                            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindByIdAsync(id, option.AllowNullReturn, option.IsTracking, cancellationToken, includeProperties);
        }

        /// <summary>
        /// Find single entity that satisfied predicate expression. Can be tracking
        /// </summary>
        /// <param name="isTracking">Tracking state of entity</param>
        /// <param name="allowNullReturn">Allow null return. If false, when value is null will throw not found exception</param>
        /// <param name="predicate">Predicate expression</param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>Domain entity matched expression or null if entity not found</returns>
        public async Task<TEntity?> FindSingleAsync(bool isTracking = false,
                                                    bool allowNullReturn = false,
                                                    Expression<Func<TEntity, bool>>? predicate = null,
                                                    CancellationToken cancellationToken = default,
                                                    params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Initialize query from the entity set
            var query = Entities.AsQueryable();
            if (includeProperties.Any())
                // Include specified properties
                query.IncludeMultiple(includeProperties);

            // Apply tracking option
            query = isTracking ? query : query.AsNoTracking();
            // Apply predicate if provided, otherwise return a single entity
            var result = predicate is not null
                ? await query.FirstOrDefaultAsync(predicate, cancellationToken)
                : await query.FirstOrDefaultAsync(cancellationToken);
            if (result is null && !allowNullReturn)
                throw new NotFoundException(MessConst.NOT_FOUND.FillArgs(new List<MessageArgs>
                {
                    new(Args.TABLE_NAME, typeof(TEntity).Name)
                }));
            return result;
        }

        /// <summary>
        /// Find all entity that satisfied predicate expression. Can be tracking
        /// </summary>
        /// <param name="isTracking">Tracking state of entity</param>
        /// <param name="predicate">Predicate expression</param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>IQueryable of entities that match predicate expression</returns>
        public IQueryable<TEntity> FindAll(bool isTracking = false,
                                           Expression<Func<TEntity, bool>>? predicate = null,
                                           params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Initialize query from the entity set
            var query = Entities.AsQueryable();
            if (includeProperties.Any())
                // Include specified properties
                query.IncludeMultiple(includeProperties);

            // Apply tracking option
            query = isTracking ? query : query.AsNoTracking();
            // Apply predicate if provided, otherwise return the query
            return predicate is not null ? query.Where(predicate) : query;
        }
    }
}
