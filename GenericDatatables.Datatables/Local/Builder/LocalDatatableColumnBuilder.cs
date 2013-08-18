using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GenericDatatables.Datatables.Html.SearchComponents;

namespace GenericDatatables.Datatables.Local.Builder
{
    public class LocalDatatableColumnBuilder<TEntity>: ILocalDatatableColumnBuilder<TEntity> where TEntity : class
    {
        private readonly ILocalDatatableBuilder<TEntity> _localDatatableBuilder;
        private readonly ILocalDatatableColumn<TEntity> _localDatatableColumn; 

        public LocalDatatableColumnBuilder(ILocalDatatableBuilder<TEntity> localDatatableBuilder, ILocalDatatableColumn<TEntity> localDatatableColumn)
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

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, bool?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, int?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, double?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, decimal?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, long?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, short?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, string>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, DateTime?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, TimeSpan?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column <TProperty>(string header, Expression<Func<TEntity, ICollection<TProperty>>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity> Display(Func<TEntity, string> display)
        {
            _localDatatableColumn.DisplayComponent = display;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> Sortable(bool sortable)
        {
            _localDatatableColumn.Sortable = sortable;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> Searchable(bool searchable)
        {
            _localDatatableColumn.Searchable = searchable;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> Visible(bool visible)
        {
            _localDatatableColumn.Visible = visible;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> Width(string width)
        {
            _localDatatableColumn.Width = width;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> Class(string @class)
        {
            _localDatatableColumn.Class = @class;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> DefaultContent(string defaultContent)
        {
            _localDatatableColumn.DefaultContent = defaultContent;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> SearchComponent(ISearchComponent searchComponent)
        {
            _localDatatableColumn.SearchComponent = searchComponent;
            return this;
        }
    }
}
