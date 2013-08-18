using System;
using System.Linq;

namespace GenericDatatables.Core.Base.Contracts
{
    /// <summary>
    ///     Defines a <see cref="Sort" /> method that enables sorting of a specified collection.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of the entity.
    /// </typeparam>
    public interface IEntitySorter <TEntity>
    {
        /// <summary>
        ///     Sorts the specified collection.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <returns>
        ///     An <see cref="IOrderedEnumerable{TEntity}" /> whose elements are sorted according to the
        ///     <see cref="ArgumentNullException" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="collection" /> is a null
        ///     reference.
        /// </exception>
        IOrderedQueryable<TEntity> Sort(IQueryable<TEntity> collection);
    }
}