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

        #region table


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("If you are seeing this in your view, you forgot to call .ToHtml() ! | Datatable: {0}", Datatable);
        }

        #endregion

        public IHtmlString ToHtml()
        {
            return ToHtml(new object());
        }

        public abstract IHtmlString ToHtml(object htmlAttributes);
    }
}