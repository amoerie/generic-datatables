using System;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;

namespace GenericDatatables.Core.Sort
{
    /// <summary>
    ///     Well I'll give you three guesses what this class does! :-)
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of entity
    /// </typeparam>
    /// <typeparam name="TProperty">
    ///     The type of the property
    /// </typeparam>
    internal class DatatablePropertySorter<TEntity, TProperty> : IDatatablePropertySorter<TEntity>
        where TEntity : class
    {
        #region Fields

        /// <summary>
        ///     The _sort.
        /// </summary>
        private readonly Expression<Func<TEntity, TProperty>> _sort;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatablePropertySorter{TEntity,TProperty}" /> class.
        /// </summary>
        /// <param name="sort">
        ///     The sort.
        /// </param>
        public DatatablePropertySorter(Expression<Func<TEntity, TProperty>> sort)
        {
            _sort = sort;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Sorts the entities in ascending order
        /// </summary>
        /// <param name="entities">
        ///     A number of entities
        /// </param>
        /// <returns>
        ///     The sorted entities
        /// </returns>
        public IOrderedQueryable<TEntity> SortAscending(IQueryable<TEntity> entities)
        {
            // expand the expression to recursively substitute expression contents. See LinqKit
            return entities.OrderBy(_sort.Expand());
        }

        /// <summary>
        ///     Sorts the entities in descending order
        /// </summary>
        /// <param name="entities">
        ///     A number of entities
        /// </param>
        /// <returns>
        ///     The sorted entities
        /// </returns>
        public IOrderedQueryable<TEntity> SortDescending(IQueryable<TEntity> entities)
        {
            // expand the expression to recursively substitute expression contents. See LinqKit
            return entities.OrderByDescending(_sort.Expand());
        }

        #endregion
    }
}