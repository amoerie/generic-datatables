using System.Collections.Generic;
using System.Linq;
using GenericDatatables.Core.Session;

namespace GenericDatatables.Core.Utilities
{
    /// <summary>
    ///     To efficiently map an entity to JSON,
    ///     this class is responsible to make a Dictionary for each entity, based on the datatable session object.
    ///     <para>
    ///     </para>
    ///     <para>
    ///         The reason I do this is firstly because I want to avoid any circular references, and secondly: dictionaries are very json friendly.
    ///     </para>
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of entity
    /// </typeparam>
    public class DatatableEntityProjector<TEntity>
        where TEntity : class
    {
        #region Fields

        /// <summary>
        ///     The _entities.
        /// </summary>
        private readonly IQueryable<TEntity> _entities;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableEntityProjector{TEntity}" /> class.
        /// </summary>
        /// <param name="entities">
        ///     The entities.
        /// </param>
        public DatatableEntityProjector(IQueryable<TEntity> entities)
        {
            _entities = entities;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The project.
        /// </summary>
        /// <param name="datatableSessionObject">
        ///     The datatable session object.
        /// </param>
        /// <returns>
        ///     The projected entity
        /// </returns>
        public IEnumerable<IDictionary<string, dynamic>> Project(DatatableSessionObject<TEntity> datatableSessionObject)
        {
            IDatatableProperty<TEntity>[] datatableProperties =
                datatableSessionObject.DatatableProperties as IDatatableProperty<TEntity>[]
                ?? datatableSessionObject.DatatableProperties.ToArray();
            foreach (TEntity entity in _entities)
            {
                var dictionary = new Dictionary<string, dynamic>();
                foreach (var property in datatableProperties)
                {
                    dictionary[property.ColumnHeader] = property.Display.Compile()(entity);
                }

                if (datatableSessionObject.HasLastColumn)
                {
                    dictionary[datatableSessionObject.LastColumnHeader] =
                        datatableSessionObject.LastColumn(entity).ToHtmlString();
                }

                yield return dictionary;
            }
        }

        #endregion
    }
}