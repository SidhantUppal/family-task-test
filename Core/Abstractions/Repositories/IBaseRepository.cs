using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using ThreadingTasks = System.Threading.Tasks;

namespace Core.Abstractions.Repositories
{
    public interface IBaseRepository<in TIdentifier, TEntity, out TRepository>
  where TEntity : class
  where TRepository : class, IBaseRepository<TIdentifier, TEntity, TRepository>
    {
        /// <summary>
        /// Get the record that has a matching Key.
        /// </summary>
        /// <param name="id">The key to search for.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="TEntity"/></returns>
        ThreadingTasks.Task<TEntity> ByIdAsync(TIdentifier id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Will return a single object, and throw ans exception if
        /// there is more than one record returned.
        /// </summary>
        /// <returns><see cref="TEntity"/></returns>
        ThreadingTasks.Task<TEntity> ToSingleAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of <see cref="TEntity"/> based on the current query.
        /// </summary>
        /// <returns><see cref="TEntity"/></returns>
        ThreadingTasks.Task<IEnumerable<TEntity>> ToListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Make the query Non Tracking
        /// </summary>
        /// <returns><see cref="IBaseRepository{TIdentifier, TEntity, TRepository}"/></returns>
        TRepository NoTrack();

        /// <summary>
        /// Create a new record of type <see cref="TEntity"/>
        /// </summary>
        /// <param name="record"></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="TEntity"/></returns>
        ThreadingTasks.Task<TEntity> CreateRecordAsync(TEntity record, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update the values for a record of type <see cref="TEntity"/>
        /// </summary>
        /// <param name="record">Object with updated values.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Number of records changed.</returns>
        ThreadingTasks.Task<int> UpdateRecordAsync(TEntity record, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete record of type <see cref="TEntity"/>
        /// </summary>
        /// <param name="id">Id for record to delete.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Count of records to delete.</returns>
        ThreadingTasks.Task<int> DeleteRecordAsync(TIdentifier id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Patch a record in the database of type <see cref="TEntity"/>
        /// </summary>
        /// <param name="id">Id for the record to patch.</param>
        /// <param name="data">json data for patching</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Count of records patched</returns>
        ThreadingTasks.Task<int> PatchRecordAsync(TIdentifier id, string data, CancellationToken cancellationToken = default);

        /// <summary>
        /// Bulk create records of type <see cref="TEntity"/>
        /// </summary>
        /// <param name="records">Records to Add</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Records added with IDs assigned</returns>
        ThreadingTasks.Task<IEnumerable<TEntity>> CreateBulkAsync(IEnumerable<TEntity> records, CancellationToken cancellationToken = default);

        /// <summary>
        /// Bulk Delete records of type <see cref="TEntity"/>
        /// </summary>
        /// <param name="ids">IDs of records to delete.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Count of Deleted Records</returns>
        ThreadingTasks.Task<int> DeleteBulkAsync(IEnumerable<TIdentifier> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks for the existence of a record based on it's ID.
        /// </summary>
        /// <param name="id">ID for checking record</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>true or false</returns>
        ThreadingTasks.Task<bool> ExistsAsync(TIdentifier id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Used to reset the Query for QueryBuilder.
        /// </summary>
        /// <returns><see cref="TRepository"/></returns>
        TRepository Reset();

        /// <summary>
        /// Used to return First or Default for the current query;
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="TEntity"/></returns>
        ThreadingTasks.Task<TEntity> SelectFirstOrDefaultAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Used to include Foreign table data
        /// </summary>
        /// <param name="includes">Foreign Table</param>
        /// <returns><see cref="TEntity"/></returns>
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
    }

}
