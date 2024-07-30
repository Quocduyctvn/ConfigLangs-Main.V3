using lscCommon.configLang.commandContract.DependencyInjection.Options;
using lscCommon.configLang.commandContract.Exceptions;
using lscCommon.configLang.commandContract.Shared;
using lscCommon.configLang.commandDomain.Abstractions.Entities;
using lscCommon.configLang.commandDomain.Abstractions.Repositories;
using lscCommon.configLang.commandPersistence.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;

namespace lscCommon.configLang.commandPersistence.Repositories
{
    /// <summary>
    /// Implementation of IGenericRepository
    /// </summary>
    /// <typeparam name="TEntity">Generic type of domain entity</typeparam>
    /// <typeparam name="TKey">Generic key of domain entity</typeparam>
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
                                                  bool allowNullReturn = true,
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
        /// Check entity with specific predicate is exist in current application
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if entity exist, otherwise false</returns>
        public Task<bool> IsExist(Expression<Func<TEntity, bool>> predicate,
                                  CancellationToken cancellationToken = default)
        {
            var query = Entities.AsQueryable();
            var result = query.Where(predicate);
            if (result.Any())
                return Task.FromResult(true);
            return Task.FromResult(false);
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

        /// <summary>
        /// Marked entity as Added state
        /// </summary>
        /// <param name="entity">Added entity</param>
        public void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        /// <summary>
        /// Marked entity as Updated state
        /// </summary>
        /// <param name="entity">Updated entity</param>
        public void Update(TEntity entity)
        {
            Entities.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Marked entity as Deleted state
        /// </summary>
        /// <param name="entity">Removed entity</param>
        public void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        /// <summary>
        /// Marked multiple entities as Deleted state
        /// </summary>
        /// <param name="entitiesToRemove">Removed entities</param>
        public void RemoveMultiple(List<TEntity> entitiesToRemove)
        {
            Entities.RemoveRange(entitiesToRemove);
        }

        /// <summary>
        /// Apply all changes in context to database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Number of changes are made to database</returns>
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in context.ChangeTracker.Entries<IEntity>()) entry.Entity.Validate();
            return context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Begin a transaction
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            return transaction.GetDbTransaction();
        }
    }
}
