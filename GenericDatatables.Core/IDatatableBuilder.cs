using System;
using System.Web;

namespace GenericDatatables.Core
{
    /// <summary>
    ///     The DatatableBuilder interface.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of entity
    /// </typeparam>
    public interface IDatatableBuilder<TEntity>
        where TEntity : class
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Constructs the HTML of the datatable.
        /// </summary>
        /// <returns>The datatable as HTML</returns>
        IHtmlString Finish();

        /// <summary>
        ///     Constructs the HTML of the datatable and also adds
        ///     a last column to the datatable in which you are free to put HTML
        ///     as you like.
        /// </summary>
        /// <param name="lastColumnHeader">
        ///     The text that should appear in the header of the last column
        /// </param>
        /// <param name="lastColumnHtml">
        ///     A lambda function that takes an instance of TEntity and uses it to generate some HTML
        /// </param>
        /// <returns>
        ///     The datatable as HTML.
        /// </returns>
        IHtmlString LastColumn(string lastColumnHeader, Func<TEntity, IHtmlString> lastColumnHtml);

        /// <summary>
        ///     Adds a property to the datatable
        /// </summary>
        /// <param name="datatableProperty">
        ///     The datatable Property.
        /// </param>
        /// <returns>
        ///     this IDatatableBuilder
        /// </returns>
        IDatatableBuilder<TEntity> Property(IDatatableProperty<TEntity> datatableProperty);

        #endregion
    }
}