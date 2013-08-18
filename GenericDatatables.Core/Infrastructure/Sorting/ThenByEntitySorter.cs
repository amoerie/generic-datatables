using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using GenericDatatables.Core.Base.Contracts;

namespace GenericDatatables.Core.Infrastructure.Sorting
{
    /// <summary>
    ///     Defines an EntitySorter for the ThenBy clause.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of the entity.
    /// </typeparam>
    /// <typeparam name="TKey">
    ///     The type of the key.
    /// </typeparam>
    [DebuggerDisplay("EntitySorter ( OrderBy: {ToString()})")]
    internal sealed class ThenByEntitySorter <TEntity, TKey> : IEntitySorter<TEntity>
    {
        /// <summary>
        ///     The _base sorter.
        /// </summary>
        private readonly IEntitySorter<TEntity> _baseSorter;

        /// <summary>
        ///     The _direction.
        /// </summary>
        private readonly SortDirection _direction;

        /// <summary>
        ///     The _key selector.
        /// </summary>
        private readonly Expression<Func<TEntity, TKey>> _keySelector;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ThenByEntitySorter{TEntity,TKey}" /> class.
        ///     Initializes a new instance of the <see cref="ThenByEntitySorter{TEntity, TKey}" /> class.
        /// </summary>
        /// <param name="baseSorter">
        ///     The base sorter.
        /// </param>
        /// <param name="keySelector">
        ///     The key selector.
        /// </param>
        /// <param name="direction">
        ///     The direction.
        /// </param>
        public ThenByEntitySorter(
            IEntitySorter<TEntity> baseSorter, Expression<Func<TEntity, TKey>> keySelector, SortDirection direction)
        {
            _baseSorter = baseSorter;
            _keySelector = keySelector;
            _direction = direction;
        }

        /// <summary>
        ///     Sorts the specified collection.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <returns>
        ///     An <see cref="IOrderedEnumerable{TEntity}" />.
        /// </returns>
        public IOrderedQueryable<TEntity> Sort(IQueryable<TEntity> collection)
        {
            IOrderedQueryable<TEntity> sortedCollection = _baseSorter.Sort(collection);

            if (_direction == SortDirection.Ascending)
            {
                return sortedCollection.ThenBy(_keySelector);
            }

            return sortedCollection.ThenByDescending(_keySelector);
        }

        /// <summary>Returns a String that represents the current object.</summary>
        /// <returns>A string representing the object.</returns>
        public override string ToString()
        {
            string sortType = _direction == SortDirection.Ascending ? string.Empty : " descending";

            return _baseSorter + ", " + _keySelector + sortType;
        }
    }
}