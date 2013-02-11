using System;
using System.Linq;
using System.Linq.Expressions;
using GenericDatatables.Core.Helper;

namespace GenericDatatables.Core.Sort
{
    /// <summary>
    ///     Class responsible for applying the property sorters
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of entity
    /// </typeparam>
    internal class DatatableSorter<TEntity>
        where TEntity : class
    {
        #region Constants

        /// <summary>
        ///     The ascending.
        /// </summary>
        private const string Ascending = "asc";

        /// <summary>
        ///     The descending.
        /// </summary>
        private const string Descending = "desc";

        #endregion

        #region Fields

        /// <summary>
        ///     The _param.
        /// </summary>
        private readonly DatatableParam _param;

        /// <summary>
        ///     The _properties.
        /// </summary>
        private readonly IDatatableProperty<TEntity>[] _properties;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableSorter{TEntity}" /> class.
        /// </summary>
        /// <param name="param">
        ///     The param.
        /// </param>
        /// <param name="properties">
        ///     The properties.
        /// </param>
        public DatatableSorter(DatatableParam param, IDatatableProperty<TEntity>[] properties)
        {
            _param = param;
            _properties = properties;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The sort.
        /// </summary>
        /// <param name="entities">
        ///     The entities.
        /// </param>
        /// <returns>
        ///     The <see cref="IQueryable" />.
        /// </returns>
        public IQueryable<TEntity> Sort(IQueryable<TEntity> entities)
        {
            IOrderedQueryable<TEntity> orderedEntities = null;
            for (int i = 0; i < _param.SortingColumnsCount; i++)
            {
                int sortingColumn = _param.SortingColumns[i];
                string sortingDirection = _param.SortDirections[i];
                IDatatableProperty<TEntity> property = _properties[sortingColumn];
                Type keyType = property.SortBy.ReturnType;

                var typeArguments = new[] {typeof (TEntity), keyType};
                IDatatablePropertySorter<TEntity> sorter = MakePropertySorter(typeArguments, property.SortBy);
                if (string.IsNullOrEmpty(sortingDirection) || sortingDirection.Equals(Ascending))
                {
                    orderedEntities = orderedEntities != null
                                          ? sorter.SortAscending(orderedEntities)
                                          : sorter.SortAscending(entities);
                }
                else if (sortingDirection.Equals(Descending))
                {
                    orderedEntities = orderedEntities != null
                                          ? sorter.SortDescending(orderedEntities)
                                          : sorter.SortDescending(entities);
                }
            }

            return orderedEntities ?? entities;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The make property sorter.
        /// </summary>
        /// <param name="typeArguments">
        ///     The type arguments.
        /// </param>
        /// <param name="sortBy">
        ///     The sort by.
        /// </param>
        /// <returns>
        ///     The <see cref="IDatatablePropertySorter" />.
        /// </returns>
        private static IDatatablePropertySorter<TEntity> MakePropertySorter(
            Type[] typeArguments, LambdaExpression sortBy)
        {
            return
                (IDatatablePropertySorter<TEntity>)
                DatatableGenericTypeHelper.Create(typeof (DatatablePropertySorter<,>))
                                          .WithTypeArguments(typeArguments)
                                          .WithConstructorArguments(new object[] {sortBy})
                                          .CreateInstance();
        }

        #endregion
    }
}