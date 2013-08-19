using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GenericDatatables.Datatables.Html.Components.DisplayComponents;
using GenericDatatables.Datatables.Html.SearchComponents;

namespace GenericDatatables.Datatables.Local.Builder
{
    public class LocalDatatableColumnBuilder<TEntity, TProperty>: ILocalDatatableColumnBuilder<TEntity, TProperty> where TEntity : class
    {
        private readonly ILocalDatatableBuilder<TEntity> _localDatatableBuilder;
        private readonly ILocalDatatableColumn<TEntity, TProperty> _localDatatableColumn;

        public LocalDatatableColumnBuilder(ILocalDatatableBuilder<TEntity> localDatatableBuilder, ILocalDatatableColumn<TEntity, TProperty> localDatatableColumn)
        {
            _localDatatableBuilder = localDatatableBuilder;
            _localDatatableColumn = localDatatableColumn;
        }

        public IHtmlString ToHtml()
        {
            return _localDatatableBuilder.ToHtml();
        }

        public IHtmlString ToHtml(object htmlAttributes)
        {
            return _localDatatableBuilder.ToHtml(htmlAttributes);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header)
        {
            return _localDatatableBuilder.Column(header);
        }

        public ILocalDatatableColumnBuilder<TEntity, bool?> Column(string header, Expression<Func<TEntity, bool?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, int?> Column(string header, Expression<Func<TEntity, int?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, double?> Column(string header, Expression<Func<TEntity, double?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, decimal?> Column(string header, Expression<Func<TEntity, decimal?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, long?> Column(string header, Expression<Func<TEntity, long?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, short?> Column(string header, Expression<Func<TEntity, short?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, string> Column(string header, Expression<Func<TEntity, string>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, DateTime?> Column(string header, Expression<Func<TEntity, DateTime?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, TimeSpan?> Column(string header, Expression<Func<TEntity, TimeSpan?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, ICollection<TNewProperty>> Column<TNewProperty>(string header, Expression<Func<TEntity, ICollection<TNewProperty>>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, TProperty> Display(Func<TEntity, string> display)
        {
            _localDatatableColumn.DisplayFunction = display;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity, TProperty> Display(Func<TEntity, IHtmlString> display)
        {
            _localDatatableColumn.DisplayFunction = entity => display(entity).ToHtmlString();
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity, TProperty> Display(IDisplayComponent<TProperty> displayComponent)
        {
            _localDatatableColumn.DisplayFunction = entity => displayComponent.ToHtml(entity, _localDatatableColumn).ToHtmlString();
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity, TProperty> Sortable(bool sortable)
        {
            _localDatatableColumn.Sortable = sortable;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity, TProperty> Searchable(bool searchable)
        {
            _localDatatableColumn.Searchable = searchable;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity, TProperty> Visible(bool visible)
        {
            _localDatatableColumn.Visible = visible;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity, TProperty> Width(string width)
        {
            _localDatatableColumn.Width = width;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity, TProperty> Class(string @class)
        {
            _localDatatableColumn.Class = @class;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity, TProperty> DefaultContent(string defaultContent)
        {
            _localDatatableColumn.DefaultContent = defaultContent;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity, TProperty> SearchComponent(ISearchComponent searchComponent)
        {
            _localDatatableColumn.SearchComponent = searchComponent;
            return this;
        }
    }
}
