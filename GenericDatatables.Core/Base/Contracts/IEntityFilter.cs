using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GenericDatatables.Core.Base.Contracts
{
    /// <summary>
    ///     Specifies a method that filters a collection by returning a filtered collection.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The element type of the collection to filter.
    /// </typeparam>
    public interface IEntityFilter <TEntity>
    {
        /// <summary>
        ///     Filters the specified collection.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <returns>
        ///     A filtered collection.
        /// </returns>
        IQueryable<TEntity> Filter(IQueryable<TEntity> collection);

        /// <summary>
        ///     Returns the predicates that have been collected into this <see cref="IEntityFilter{TEntity}"/>
        /// </summary>
        IEnumerable<Expression<Func<TEntity, bool>>> Predicates { get; } 

    }
}