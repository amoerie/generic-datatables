using System;
using System.Linq;
using GenericDatatables.Core.Filter;
using GenericDatatables.Core.Session;
using GenericDatatables.Core.Sort;
using GenericDatatables.Core.Utilities;

namespace GenericDatatables.Core
{
    /// <summary>
    ///     One of the main classes.
    ///     The parser is basically responsible for taking a DatatablesParam, an IQueryable of entities, and then parsing that to a DatatablesReply
    ///     However, this is more of a coordinating object. The actual magic happens in all the helper classes.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of entity
    /// </typeparam>
    public class DatatableParser<TEntity>
        where TEntity : class
    {
        #region Fields

        /// <summary>
        ///     The list of properties of the datatable
        /// </summary>
        private readonly IDatatableProperty<TEntity>[] _properties;

        /// <summary>
        ///     The corresponding session object of the datatable
        /// </summary>
        private readonly DatatableSessionObject<TEntity> _sessionObject;

        /// <summary>
        ///     The entities that should be used as a data source for the datatable
        /// </summary>
        private IQueryable<TEntity> _entities;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableParser{TEntity}" /> class.
        /// </summary>
        /// <param name="entities">
        ///     The entities.
        /// </param>
        /// <param name="sessionObject">
        ///     The session object.
        /// </param>
        public DatatableParser(IQueryable<TEntity> entities, DatatableSessionObject<TEntity> sessionObject)
        {
            _entities = entities;
            _properties = sessionObject.DatatableProperties.ToArray();
            _sessionObject = sessionObject;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Applies the desired sorting, filtering and pagination logic to the
        ///     data source entities according to the param object and returns a reply
        /// </summary>
        /// <param name="param">
        ///     The parameter containing the sorting, filtering and pagination logic
        /// </param>
        /// <returns>
        ///     A parsed collection of entities wrapped in a reply object
        /// </returns>
        public DatatableReply Parse(DatatableParam param)
        {
            int totalRecords = _entities.Count();
            _entities = Sort(param);
            _entities = FilterGlobal(param);
            _entities = FilterSpecific(param);
            int displayRecords = _entities.Count();
            _entities = _entities.Skip(param.DisplayStart);
            _entities = _entities.Take(param.DisplayLength);

            var projector = new DatatableEntityProjector<TEntity>(_entities);
            var reply = new DatatableReply
                {
                    Echo = Convert.ToInt32(param.Echo),
                    Columns = string.Join(",", _properties.Select(p => p.ColumnHeader)),
                    TotalRecords = totalRecords,
                    TotalDisplayRecords = displayRecords,
                    Data = projector.Project(_sessionObject).ToArray()
                };
            return reply;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Applies the global filter, if any, to the entities
        /// </summary>
        /// <param name="param">
        ///     The param containing the global filter
        /// </param>
        /// <returns>
        ///     the filtered entities
        /// </returns>
        private IQueryable<TEntity> FilterGlobal(DatatableParam param)
        {
            var filter = new DatatableFilter<TEntity>(param, _properties);
            return filter.Filter(_entities);
        }

        /// <summary>
        ///     Applies property-specific filters, if any, to the entities
        /// </summary>
        /// <param name="param">
        ///     The param containing the property filters
        /// </param>
        /// <returns>
        ///     the filtered entities
        /// </returns>
        private IQueryable<TEntity> FilterSpecific(DatatableParam param)
        {
            var filter = new DatatablePropertyFilter<TEntity>(param, _properties);
            return filter.Filter(_entities);
        }

        /// <summary>
        ///     Sorts the entities by the desired properties in their proper order
        /// </summary>
        /// <param name="param">
        ///     The param containing the sorting logic
        /// </param>
        /// <returns>
        ///     the sorted entities
        /// </returns>
        private IQueryable<TEntity> Sort(DatatableParam param)
        {
            var sorter = new DatatableSorter<TEntity>(param, _properties);
            return sorter.Sort(_entities);
        }

        #endregion
    }
}