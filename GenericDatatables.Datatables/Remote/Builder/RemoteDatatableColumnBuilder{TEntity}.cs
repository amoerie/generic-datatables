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
    public class RemoteDatatableColumnBuilder<TEntity> : RemoteDatatableColumnBuilderBase<TEntity>, IRemoteDatatableColumnBuilder<TEntity> where TEntity : Entity
    {
        private readonly RemoteDatatableColumn<TEntity> _remoteDatatableColumn;

        internal RemoteDatatableColumnBuilder(IRemoteDatatableBuilder<TEntity> remoteDatatableBuilder, RemoteDatatableColumn<TEntity> remoteDatatableColumn): base(remoteDatatableBuilder)
        {
            _remoteDatatableColumn = remoteDatatableColumn;
        }
        
        public IRemoteDatatableColumnBuilder<TEntity> Display(Func<TEntity, string> display)
        {
            _remoteDatatableColumn.DisplayFunction = display;
            return this;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Display(Func<TEntity, IHtmlString> display)
        {
            _remoteDatatableColumn.DisplayFunction = entity => display(entity).ToHtmlString();
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


    }
}
