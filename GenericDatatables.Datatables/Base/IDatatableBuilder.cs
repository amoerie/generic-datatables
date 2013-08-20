using System.Web;
using GenericDatatables.Core.Infrastructure.Attributes.JetBrains.Annotations;

namespace GenericDatatables.Datatables.Base
{
    public interface IDatatableBuilder
    {
        #region table

        /// <summary>
        ///     Renders the html for the constructed <see cref="Datatable{TEntity}"/>
        /// </summary>
        /// <returns>The <see cref="IHtmlString"/> that contains the html for the constructed <see cref="Datatable{TEntity}"/></returns>
        IHtmlString ToHtml();

        /// <summary>
        ///     Renders the html for the constructed <see cref="Datatable{TEntity}"/>
        /// </summary>
        /// <param name="htmlAttributes">The html attributes that should be added to the table element</param>
        /// <returns>The <see cref="IHtmlString"/> that contains the html for the constructed <see cref="Datatable{TEntity}"/></returns>
        IHtmlString ToHtml([NotNull] object htmlAttributes);

        #endregion
    }
}