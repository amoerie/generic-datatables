using System.Web;
using System.Web.Mvc;

namespace GenericDatatables.Datatables.Base
{
    public abstract class DatatableBuilder<TEntity> : IDatatableBuilder<TEntity> where TEntity : class
    {
        protected readonly Datatable<TEntity> Datatable;
        protected readonly HtmlHelper HtmlHelper;

        protected DatatableBuilder(HtmlHelper htmlHelper, Datatable<TEntity> datatable)
        {
            Datatable = datatable;
            HtmlHelper = htmlHelper;
        }

        public override string ToString()
        {
            return string.Format("If you are seeing this in your view, you forgot to call .ToHtml() ! | Datatable: {0}", Datatable);
        }

        public IHtmlString ToHtml()
        {
            return ToHtml(new object());
        }

        public abstract IHtmlString ToHtml(object htmlAttributes);
    }
}