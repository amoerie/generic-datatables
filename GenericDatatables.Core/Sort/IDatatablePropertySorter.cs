using System.Linq;

namespace GenericDatatables.Core.Sort
{
    /// <summary>
    ///     Interface for sorting the properties of a datatable
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of entity
    /// </typeparam>
    public interface IDatatablePropertySorter<TEntity>
        where TEntity : class
    {
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
        IOrderedQueryable<TEntity> SortAscending(IQueryable<TEntity> entities);

        /// <summary>
        ///     Sorts the entities in descending order
        /// </summary>
        /// <param name="entities">
        ///     A number of entities
        /// </param>
        /// <returns>
        ///     The sorted entities
        /// </returns>
        IOrderedQueryable<TEntity> SortDescending(IQueryable<TEntity> entities);

        #endregion
    }
}