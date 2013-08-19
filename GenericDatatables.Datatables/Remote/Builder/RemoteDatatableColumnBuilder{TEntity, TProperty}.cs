using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Datatables.Html.Components.DisplayComponents;
using GenericDatatables.Datatables.Html.SearchComponents;
using GenericDatatables.Datatables.Remote.Filtering;

namespace GenericDatatables.Datatables.Remote.Builder
{
    public class RemoteDatatableColumnBuilder<TEntity, TProperty>: IRemoteDatatableColumnBuilder<TEntity, TProperty> where TEntity : Entity
    {
        private readonly IRemoteDatatableBuilder<TEntity> _remoteDatatableBuilder;
        private readonly RemoteDatatableColumn<TEntity, TProperty> _remoteDatatableColumn;

        internal RemoteDatatableColumnBuilder(IRemoteDatatableBuilder<TEntity> remoteDatatableBuilder, RemoteDatatableColumn<TEntity, TProperty> remoteDatatableColumn)
        {
            _remoteDatatableBuilder = remoteDatatableBuilder;
            _remoteDatatableColumn = remoteDatatableColumn;
        }

        public IRemoteDatatableColumnBuilder<TEntity, TProperty> Display(Func<TEntity, string> display)
        {
            _remoteDatatableColumn.DisplayFunction = display;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity, TProperty> Display(Func<TEntity, IHtmlString> display)
        {
            _remoteDatatableColumn.DisplayFunction = entity => display(entity).ToHtmlString();
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity, TProperty> Display(IDisplayComponent<TProperty> displayComponent)
        {
            _remoteDatatableColumn.DisplayFunction = entity => displayComponent.ToHtml(entity, _remoteDatatableColumn).ToHtmlString();
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity, TProperty> Search(Expression<Func<TEntity, string, bool>> searchFilter)
        {
            _remoteDatatableColumn.Searchable = true;
            _remoteDatatableColumn.PropertyFilter = new DatatablePropertyFilter<TEntity, string>
            {
                SearchParser = search => search,
                SearchFilter = searchFilter
            };
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity, TProperty> Search<TSearch>(Func<string, TSearch> searchParser, Expression<Func<TEntity, TSearch, bool>> searchFilter)
        {
            _remoteDatatableColumn.Searchable = true;
            _remoteDatatableColumn.PropertyFilter = new DatatablePropertyFilter<TEntity, TSearch>
            {
                SearchParser = searchParser,
                SearchFilter = searchFilter
            };
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity, TProperty> Sortable(bool sortable)
        {
            _remoteDatatableColumn.Sortable = sortable;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity, TProperty> Searchable(bool searchable)
        {
            _remoteDatatableColumn.Searchable = searchable;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity, TProperty> Visible(bool visible)
        {
            _remoteDatatableColumn.Visible = visible;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity, TProperty> Width(string width)
        {
            _remoteDatatableColumn.Width = width;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity, TProperty> Class(string @class)
        {
            _remoteDatatableColumn.Class = @class;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity, TProperty> DefaultContent(string defaultContent)
        {
            _remoteDatatableColumn.DefaultContent = defaultContent;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity, TProperty> SearchComponent(ISearchComponent searchComponent)
        {
            _remoteDatatableColumn.SearchComponent = searchComponent;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header)
        {
            return _remoteDatatableBuilder.Column(header);
        }

        public IRemoteDatatableColumnBuilder<TEntity, bool?> Column(string header, Expression<Func<TEntity, bool?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, int?> Column(string header, Expression<Func<TEntity, int?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, double?> Column(string header, Expression<Func<TEntity, double?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, decimal?> Column(string header, Expression<Func<TEntity, decimal?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, long?> Column(string header, Expression<Func<TEntity, long?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, short?> Column(string header, Expression<Func<TEntity, short?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, string> Column(string header, Expression<Func<TEntity, string>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, DateTime?> Column(string header, Expression<Func<TEntity, DateTime?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, TimeSpan?> Column(string header, Expression<Func<TEntity, TimeSpan?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, ICollection<TProperty1>> Column <TProperty1>(string header, Expression<Func<TEntity, ICollection<TProperty1>>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IHtmlString ToHtml()
        {
            return _remoteDatatableBuilder.ToHtml();
        }

        public IHtmlString ToHtml(object htmlAttributes)
        {
            return _remoteDatatableBuilder.ToHtml(htmlAttributes);
        }
    }
}
