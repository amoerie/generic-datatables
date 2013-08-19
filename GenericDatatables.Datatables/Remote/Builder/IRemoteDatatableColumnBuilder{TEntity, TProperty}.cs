using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Core.Infrastructure.Attributes.JetBrains.Annotations;
using GenericDatatables.Datatables.Html.Components.DisplayComponents;
using GenericDatatables.Datatables.Html.SearchComponents;

namespace GenericDatatables.Datatables.Remote.Builder
{
    public interface IRemoteDatatableColumnBuilder<TEntity, TProperty>: IRemoteDatatableBuilder<TEntity> where TEntity : Entity
    {
        IRemoteDatatableColumnBuilder<TEntity, TProperty> Display([NotNull] Func<TEntity, string> display);

        IRemoteDatatableColumnBuilder<TEntity, TProperty> Display([NotNull] Func<TEntity, IHtmlString> display);

        IRemoteDatatableColumnBuilder<TEntity, TProperty> Display([NotNull] IDisplayComponent<TProperty> displayComponent);

        IRemoteDatatableColumnBuilder<TEntity, TProperty> Search([NotNull] Expression<Func<TEntity, string, bool>> searchFilter);

        IRemoteDatatableColumnBuilder<TEntity, TProperty> Search<TSearch>([NotNull] Func<string, TSearch> searchParser, [NotNull] Expression<Func<TEntity, TSearch, bool>> searchFilter);

        IRemoteDatatableColumnBuilder<TEntity, TProperty> Sortable(bool sortable);

        IRemoteDatatableColumnBuilder<TEntity, TProperty> Searchable(bool searchable);

        IRemoteDatatableColumnBuilder<TEntity, TProperty> Visible(bool visible);

        IRemoteDatatableColumnBuilder<TEntity, TProperty> Width([NotNull] string width);

        IRemoteDatatableColumnBuilder<TEntity, TProperty> Class([NotNull] string @class);

        IRemoteDatatableColumnBuilder<TEntity, TProperty> DefaultContent([NotNull] string defaultContent);

        IRemoteDatatableColumnBuilder<TEntity, TProperty> SearchComponent([NotNull] ISearchComponent searchComponent);
    }
}
