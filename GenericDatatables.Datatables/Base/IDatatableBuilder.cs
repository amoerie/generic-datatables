using System.Web;
using GenericDatatables.Core.Infrastructure.Attributes.JetBrains.Annotations;

namespace GenericDatatables.Datatables.Base
{
    public interface IDatatableBuilder<TEntity> where TEntity : class
    {
        #region table

        IHtmlString ToHtml();

        IHtmlString ToHtml([NotNull] object htmlAttributes);

        #endregion
    }
}