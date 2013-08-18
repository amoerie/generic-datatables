using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Datatables.Html.SearchComponents;
using GenericDatatables.Datatables.Remote.Filtering;
using GenericDatatables.Datatables.Remote.Sorting;

namespace GenericDatatables.Datatables.Remote.Builder
{
    public class RemoteDatatableColumnBuilder<TEntity> : IRemoteDatatableColumnBuilder<TEntity> where TEntity : Entity
    {
        private readonly IRemoteDatatableBuilder<TEntity> _remoteDatatableBuilder;
        private readonly RemoteDatatableColumn<TEntity> _remoteDatatableColumn;

        internal RemoteDatatableColumnBuilder(IRemoteDatatableBuilder<TEntity> remoteDatatableBuilder, RemoteDatatableColumn<TEntity> remoteDatatableColumn)
        {
            _remoteDatatableBuilder = remoteDatatableBuilder;
            _remoteDatatableColumn = remoteDatatableColumn;
        }
        
        public IRemoteDatatableColumnBuilder<TEntity> Display(Func<TEntity, string> display)
        {
            _remoteDatatableColumn.DisplayComponent = display;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Sort<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            _remoteDatatableColumn.Sortable = true;
            _remoteDatatableColumn.PropertySorters.Add(new DatatablePropertySorter<TEntity, TProperty>(property));
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Search(Expression<Func<TEntity, string, bool>> searchFilter)
        {
            _remoteDatatableColumn.Searchable = true;
            _remoteDatatableColumn.PropertyFilter = new DatatablePropertyFilter<TEntity, string>
            {
                SearchParser = search => search,
                SearchFilter = searchFilter
            };
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Search<TSearch>(Func<string, TSearch> searchParser, Expression<Func<TEntity, TSearch, bool>> searchFilter)
        {
            _remoteDatatableColumn.Searchable = true;
            _remoteDatatableColumn.PropertyFilter = new DatatablePropertyFilter<TEntity, TSearch>
            {
                SearchParser = searchParser,
                SearchFilter = searchFilter
            };
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Sortable(bool sortable)
        {
            _remoteDatatableColumn.Sortable = sortable;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Searchable(bool searchable)
        {
            _remoteDatatableColumn.Searchable = searchable;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Visible(bool visible)
        {
            _remoteDatatableColumn.Visible = visible;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Width(string width)
        {
            _remoteDatatableColumn.Width = width;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Class(string @class)
        {
            _remoteDatatableColumn.Class = @class;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> DefaultContent(string defaultContent)
        {
            _remoteDatatableColumn.DefaultContent = defaultContent;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> SearchComponent(ISearchComponent searchComponent)
        {
            _remoteDatatableColumn.SearchComponent = searchComponent;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header)
        {
            return _remoteDatatableBuilder.Column(header);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, bool?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, int?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, double?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, decimal?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, long?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, short?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, string>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, DateTime?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, TimeSpan?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column <TProperty>(string header, Expression<Func<TEntity, ICollection<TProperty>>> propertyExpression)
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
