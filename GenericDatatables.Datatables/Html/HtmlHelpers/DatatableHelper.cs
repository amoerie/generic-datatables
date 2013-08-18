using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Datatables.Local;
using GenericDatatables.Datatables.Local.Builder;
using GenericDatatables.Datatables.Remote;
using GenericDatatables.Datatables.Remote.Builder;

namespace GenericDatatables.Datatables.Html.HtmlHelpers
{
    public static class DatatableHelper
    {
        #region Remote

        /// <summary>
        ///     Returns a <see cref="IRemoteDatatableBuilder{TEntity}" /> to further configure the datatable
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="html">The html helper</param>
        /// <param name="datatableUrl">The url from where the data can be fetched</param>
        /// <returns>
        ///     a <see cref="IRemoteDatatableBuilder{TEntity}" /> to further configure the datatable
        /// </returns>
        public static IRemoteDatatableBuilder<TEntity> Datatable<TEntity>(this HtmlHelper html, string datatableUrl)
            where TEntity : Entity
        {
            return html.Datatable<TEntity>(datatableUrl, HttpUtility.HtmlAttributeEncode(string.Format("Datatable_{0}", typeof (TEntity).Name)));
        }

        /// <summary>
        ///     Returns a <see cref="IRemoteDatatableBuilder{TEntity}" /> to further configure the datatable
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="html">The html helper</param>
        /// <param name="datatableUrl">The url from where the data can be fetched</param>
        /// <param name="datatableId">
        ///     The datatable id. This will be set as the 'id' attribute on the HTML 'table' and will also be used to identify this datatable in the
        ///     <see
        ///         cref="DatatableStorage" />
        /// </param>
        /// <returns>
        ///     a <see cref="IRemoteDatatableBuilder{TEntity}" /> to further configure the datatable
        /// </returns>
        public static IRemoteDatatableBuilder<TEntity> Datatable<TEntity>(this HtmlHelper html,
                                                                          string datatableUrl,
                                                                          string datatableId)
            where TEntity : Entity
        {
            if (datatableUrl == null)
                throw new ArgumentNullException("datatableUrl");
            if (datatableId == null)
                throw new ArgumentNullException("datatableId");

            return new RemoteDatatableBuilder<TEntity>(html, new RemoteDatatable<TEntity>
                {
                    Id = HttpUtility.HtmlAttributeEncode(datatableId),
                    Url = datatableUrl
                });
        }

        #endregion

        /// <summary>
        ///     Returns a <see cref="ILocalDatatableBuilder{TEntity}" /> to further configure the datatable
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="html">The html helper</param>
        /// <param name="entities">The data to fill this datatable with</param>
        /// <returns>
        ///     a <see cref="ILocalDatatableBuilder{TEntity}" /> to further configure the datatable
        /// </returns>
        public static ILocalDatatableBuilder<TEntity> Datatable<TEntity>(this HtmlHelper html,
                                                                         IEnumerable<TEntity> entities) where TEntity : class
        {
            return html.Datatable(entities, HttpUtility.HtmlAttributeEncode(string.Format("Datatable_{0}", typeof (TEntity).Name)));
        }

        /// <summary>
        ///     Returns a <see cref="ILocalDatatableBuilder{TEntity}" /> to further configure the datatable
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="html">The html helper</param>
        /// <param name="entities">The data to fill this datatable with</param>
        /// <param name="datatableId">The datatable id. This will be set as the 'id' attribute on the HTML 'table'</param>
        /// <returns>
        ///     a <see cref="ILocalDatatableBuilder{TEntity}" /> to further configure the datatable
        /// </returns>
        public static ILocalDatatableBuilder<TEntity> Datatable<TEntity>(this HtmlHelper html,
                                                                         IEnumerable<TEntity> entities,
                                                                         string datatableId) where TEntity : class
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            if (datatableId == null)
                throw new ArgumentNullException("datatableId");

            return new LocalDatatableBuilder<TEntity>(html, new LocalDatatable<TEntity>
                {
                    Entities = entities,
                    Id = datatableId
                });
        }
    }
}