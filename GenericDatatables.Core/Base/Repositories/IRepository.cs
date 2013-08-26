using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using GenericDatatables.Core.Base.Contracts;

namespace GenericDatatables.Core.Base.Repositories
{
    public interface IRepository <TEntity>
        where TEntity : class, IDeletable, IIdentifiable
    {
        /// <summary>
        ///     Returns the <see cref="IQueryable{TEntity}"/> of entities ordered with the <paramref name="sorter" />,
        ///     filtered by the <paramref name="filter" />, paged accordingly with the <paramref name="page" /> and
        ///     <paramref
        ///         name="pageSize" />
        ///     and with navigational properties eagerly loaded as defined by the <paramref name="includer" />.
        /// </summary>
        /// 
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TEntity}" /> that defines how the entities should be filtered
        /// </param>
        /// <param name="sorter">
        ///     The <see cref="IEntitySorter{TEntity}" /> that defines how the entities should be sorted
        /// </param>
        /// <param name="page">
        ///     The (0-based) page number. The number of entities that will be skipped is equal to the page * pageSize
        /// </param>
        /// <param name="pageSize">
        ///     The number of entities this method (maximally) returns.
        /// </param>
        /// <param name="includer">
        ///     The <see cref="IEntityIncluder{TEntity}" /> that defines which navigational properties should be eagerly loaded.
        /// </param>
        /// 
        /// <exception cref="ArgumentException">
        ///     If a <paramref name="page" /> or <paramref name="pageSize" /> is defined but <paramref name="sorter" /> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     If a <paramref name="page" /> is defined but <paramref name="pageSize" /> is null
        /// </exception>
        /// 
        /// <returns>
        ///     the <see cref="IQueryable{TEntity}"/> of entities ordered with the <paramref name="sorter" />,
        ///     filtered by the <paramref name="filter" />, paged accordingly with the <paramref name="page" /> and
        ///     <paramref
        ///         name="pageSize" />
        ///     and with navigational properties eagerly loaded as defined by the <paramref name="includer" />
        /// </returns>
        IQueryable<TEntity> List(
            IEntityFilter<TEntity> filter = default( IEntityFilter<TEntity> ),
            IEntitySorter<TEntity> sorter = default( IEntitySorter<TEntity> ),
            int? page = null,
            int? pageSize = null,
            IEntityIncluder<TEntity> includer = default( IEntityIncluder<TEntity> ));

        /// <summary>
        ///     Returns the number of entities that satisfy the filter, or the total number of entities if no filter is defined
        /// </summary>
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TEntity}" /> that defines how the entities should be filtered
        /// </param>
        /// <returns>the number of entities that satisfy the filter, or the total number of entities if no filter is defined</returns>
        int Count(IEntityFilter<TEntity> filter = default( IEntityFilter<TEntity> ));

        /// <summary>
        ///     Returns true if any entity in the collection satisfies the <paramref name="filter" /> or false otherwise
        /// </summary>
        /// 
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TEntity}" /> that defines how the entities should be filtered.
        /// </param>
        /// 
        /// <returns>
        ///     True if any entity in the collection satisfies the <paramref name="filter" /> or false otherwise
        /// </returns>
        bool Any(IEntityFilter<TEntity> filter = default( IEntityFilter<TEntity> ));

        /// <summary>
        ///     Returns the single entity that satisfies the predicate or null if none were found.
        ///     Throws an exception if more than one entity is found.
        /// </summary>
        /// 
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TEntity}" /> that defines how the entities should be filtered
        /// </param>
        /// <param name="includer">
        ///     The <see cref="IEntityIncluder{TEntity}" /> that defines which navigational properties should be eagerly loaded.
        /// </param>
        /// 
        /// <exception cref="InvalidOperationException">
        ///     There was more than one entity that satisfied the filter
        /// </exception>
        /// 
        /// <returns>
        ///     The <see cref="TEntity" />.
        /// </returns>
        TEntity SingleOrDefault(IEntityFilter<TEntity> filter = default( IEntityFilter<TEntity> ), IEntityIncluder<TEntity> includer = default( IEntityIncluder<TEntity> ));

        /// <summary>
        ///     Returns the single entity that satisfies the predicate.
        ///     Throws an exception if none or more than one entity is found.
        /// </summary>
        /// 
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TEntity}" /> that defines how the entities should be filtered
        /// </param>
        /// <param name="includer">
        ///     The <see cref="IEntityIncluder{TEntity}" /> that defines which navigational properties should be eagerly loaded.
        /// </param>
        /// 
        /// <exception cref="InvalidOperationException">
        ///     There were no entities that satisfied the filter
        ///     - or -
        ///     There was more than one entity that satisfied the filter
        /// </exception>
        /// 
        /// <returns>
        ///     The <see cref="TEntity" />.
        /// </returns>
        TEntity Single(IEntityFilter<TEntity> filter = default( IEntityFilter<TEntity> ), IEntityIncluder<TEntity> includer = default( IEntityIncluder<TEntity> ));

        /// <summary>
        ///     Returns the first entity from that satisfies the filter or null otherwise
        /// </summary>
        /// 
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TEntity}" /> that defines how the entities should be filtered
        /// </param>
        /// <param name="sorter">
        ///     The <see cref="IEntitySorter{TEntity}" /> that defines how the entities should be sorted
        /// </param>
        /// <param name="includer">
        ///     The <see cref="IEntityIncluder{TEntity}" /> that defines which navigational properties should be eagerly loaded.
        /// </param>
        /// 
        /// <returns>
        ///     The first entity that satisfies the <paramref name="filter"/> or null otherwise
        /// </returns>
        TEntity FirstOrDefault(
            IEntityFilter<TEntity> filter = default( IEntityFilter<TEntity> ),
            IEntitySorter<TEntity> sorter = default( IEntitySorter<TEntity> ),
            IEntityIncluder<TEntity> includer = default( IEntityIncluder<TEntity> ));

        /// <summary>
        ///     Returns the first entity from that satisfies the filter
        /// </summary>
        /// 
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TEntity}" /> that defines how the entities should be filtered
        /// </param>
        /// <param name="sorter">
        ///     The <see cref="IEntitySorter{TEntity}" /> that defines how the entities should be sorted
        /// </param>
        /// <param name="includer">
        ///     The <see cref="IEntityIncluder{TEntity}" /> that defines which navigational properties should be eagerly loaded.
        /// </param>
        /// 
        /// <exception cref="InvalidOperationException">
        ///     There was more than one entity that satisfied the filter
        /// </exception>
        /// 
        /// <returns>
        ///     The first entity that satisfies the <paramref name="filter"/> or null otherwise
        /// </returns>
        TEntity First(
            IEntityFilter<TEntity> filter = default( IEntityFilter<TEntity> ),
            IEntitySorter<TEntity> sorter = default( IEntitySorter<TEntity> ),
            IEntityIncluder<TEntity> includer = default( IEntityIncluder<TEntity> ));

        /// <summary>
        ///     Returns the entities ordered with the <paramref name="sorter" />,
        ///     filtered by the <paramref name="filter" /> and with navigational properties eagerly loaded as defined by the <paramref name="includer" />.
        ///     Finally, these entities are processed by the <paramref name="selector"/>
        /// </summary>
        /// <param name="selector">
        ///     The <see cref="Func{TEntity,TResult}"/> that transforms each <typeparamref name="TEntity"/> from the result set into a <typeparamref name="TResult"/>
        /// </param>
        /// <param name="filter">
        ///     The <see cref="IEntityFilter{TEntity}" /> that defines how the entities should be filtered
        /// </param>
        /// <param name="sorter">
        ///     The <see cref="IEntitySorter{TEntity}" /> that defines how the entities should be sorted
        /// </param>
        /// <param name="includer">
        ///     The <see cref="IEntityIncluder{TEntity}" /> that defines which navigational properties should be eagerly loaded.
        /// </param>
        /// 
        /// <typeparam name="TResult">
        ///     The type of the result
        /// </typeparam>
        /// 
        /// <returns>
        ///     The entities ordered with the <paramref name="sorter" />,
        ///     filtered by the <paramref name="filter" /> and with navigational properties eagerly loaded as defined by the <paramref name="includer" />.
        ///     Finally, these entities are processed by the <paramref name="selector"/>
        /// </returns>
        IEnumerable<TResult> Select <TResult>(
            Func<TEntity, TResult> selector,
            IEntityFilter<TEntity> filter = default( IEntityFilter<TEntity> ),
            IEntitySorter<TEntity> sorter = default( IEntitySorter<TEntity> ),
            IEntityIncluder<TEntity> includer = default( IEntityIncluder<TEntity> ));

        /// <summary>
        ///     Returns the entity for the given keyValues.
        ///     Throws an exception if more than one entity is found.
        /// </summary>
        /// 
        /// <param name="id">
        ///     The id.
        /// </param>
        /// 
        /// <returns>
        ///     The <see cref="TEntity" />.
        /// </returns>
        TEntity Find(int id);

        /// <summary>
        ///     Queries the entities of this repository and returns the result of that query. 
        /// </summary>
        /// 
        /// <param name="query">
        ///     The query that specifies what to do with the <see cref="EntitySet{TEntity}"/> of <typeparamref name="TEntity"/>
        /// </param>
        /// 
        /// <typeparam name="TResult">
        ///     The return type of the query
        /// </typeparam>
        /// 
        /// <returns>
        ///     The result of the <paramref name="query"/>
        /// </returns>
        TResult Query<TResult>(Func<IQueryable<TEntity>, TResult> query);

        /// <summary>
        ///     Adds an entity to the collection or updates the corresponding entity from the database if its Id is not equal to zero.
        /// </summary>
        /// <param name="entity">
        ///     The entity to add or updates
        /// </param>
        void AddOrUpdate(TEntity entity);

        /// <summary>
        ///     Soft deletes an entity from the collection (sets boolean deleted to true).
        /// </summary>
        /// <param name="entity">
        ///     The entity to delete
        /// </param>
        void Delete(TEntity entity);

        /// <summary>
        ///     Soft deletes entities from the collection (sets boolean deleted to true)
        ///     If one or more entities are already deleted, it will skip them.
        /// </summary>
        /// <param name="entities">
        ///     The entities
        /// </param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        ///     Soft deletes an entity from the collection by its id (sets boolean deleted to true).
        /// </summary>
        /// <param name="id">
        ///     The id of the entity that needs to be deleted
        /// </param>
        void Delete(int id);

        /// <summary>
        ///     Actually deletes the entity from the database (watch out for cascading rules).
        /// </summary>
        /// <param name="entity">
        ///     The entity to delete
        /// </param>
        void HardDelete(TEntity entity);

        /// <summary>
        ///     Actually deletes the entity from the database by its id (watch out for cascading rules).
        /// </summary>
        /// <param name="id">
        ///     The id of the entity to delete
        /// </param>
        void HardDelete(int id);
    }
}