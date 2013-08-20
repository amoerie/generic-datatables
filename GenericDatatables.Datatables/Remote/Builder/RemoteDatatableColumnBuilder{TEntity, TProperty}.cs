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
    public class RemoteDatatableColumnBuilder<TEntity, TProperty>: RemoteDatatableColumnBuilderBase<TEntity>, IRemoteDatatableColumnBuilder<TEntity, TProperty> where TEntity : Entity
    {
        private readonly RemoteDatatableColumn<TEntity, TProperty> _remoteDatatableColumn;

        internal RemoteDatatableColumnBuilder(IRemoteDatatableBuilder<TEntity> remoteDatatableBuilder, RemoteDatatableColumn<TEntity, TProperty> remoteDatatableColumn)
            : base(remoteDatatableBuilder)
        {
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
    }
}
