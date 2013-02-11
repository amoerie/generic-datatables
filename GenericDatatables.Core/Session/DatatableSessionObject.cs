using System;
using System.Collections.Generic;
using System.Web;

namespace GenericDatatables.Core.Session
{
    /// <summary>
    ///     Contains all necessary information about a specific datatable
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of entity
    /// </typeparam>
    public class DatatableSessionObject<TEntity>
        where TEntity : class
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the datatable properties.
        /// </summary>
        public IEnumerable<IDatatableProperty<TEntity>> DatatableProperties { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this datatable has a last column.
        /// </summary>
        public bool HasLastColumn { get; set; }

        /// <summary>
        ///     Gets or sets the last column.
        /// </summary>
        public Func<TEntity, IHtmlString> LastColumn { get; set; }

        /// <summary>
        ///     Gets or sets the last column header.
        /// </summary>
        public string LastColumnHeader { get; set; }

        #endregion
    }
}