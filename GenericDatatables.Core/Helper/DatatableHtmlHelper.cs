using System.Web.Mvc;

namespace GenericDatatables.Core.Helper
{
    /// <summary>
    ///     The datatable html helper.
    /// </summary>
    public static class DatatableHtmlHelper
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Access point for the view. Returns a new IDatatableBuilder
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The entity type
        /// </typeparam>
        /// <param name="htmlHelper">
        ///     This htmlhelper
        /// </param>
        /// <param name="datatableId">
        ///     Unique id for this datatable. Will be used to store info in session
        /// </param>
        /// <returns>
        ///     a new datatablebuilder
        /// </returns>
        public static IDatatableBuilder<TEntity> Datatable<TEntity>(this HtmlHelper htmlHelper, string datatableId)
            where TEntity : class
        {
            return new DatatableBuilder<TEntity>(datatableId, htmlHelper.ViewContext.HttpContext.Session);
        }

        #endregion
    }
}