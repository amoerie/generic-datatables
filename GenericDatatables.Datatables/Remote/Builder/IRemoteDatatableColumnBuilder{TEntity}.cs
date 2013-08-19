using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Core.Infrastructure.Attributes.JetBrains.Annotations;
using GenericDatatables.Datatables.Html.SearchComponents;

namespace GenericDatatables.Datatables.Remote.Builder
{
    public interface IRemoteDatatableColumnBuilder<TEntity> : IRemoteDatatableBuilder<TEntity> where TEntity : Entity
    {
        IRemoteDatatableColumnBuilder<TEntity> Display([NotNull] Func<TEntity, string> display);

        IRemoteDatatableColumnBuilder<TEntity> Display([NotNull] Func<TEntity, IHtmlString> display);

        IRemoteDatatableColumnBuilder<TEntity> Sort<TProperty>([NotNull] Expression<Func<TEntity, TProperty>> property);

        IRemoteDatatableColumnBuilder<TEntity> Search([NotNull] Expression<Func<TEntity, string, bool>> searchFilter);

        IRemoteDatatableColumnBuilder<TEntity> Search<TSearch>([NotNull] Func<string, TSearch> searchParser, [NotNull] Expression<Func<TEntity, TSearch, bool>> searchFilter);

        IRemoteDatatableColumnBuilder<TEntity> Sortable(bool sortable);

        IRemoteDatatableColumnBuilder<TEntity> Searchable(bool searchable);

        IRemoteDatatableColumnBuilder<TEntity> Visible(bool visible);

        IRemoteDatatableColumnBuilder<TEntity> Width([NotNull] string width);

        IRemoteDatatableColumnBuilder<TEntity> Class([NotNull] string @class);

        IRemoteDatatableColumnBuilder<TEntity> DefaultContent([NotNull] string defaultContent);

        IRemoteDatatableColumnBuilder<TEntity> SearchComponent([NotNull] ISearchComponent searchComponent);
    }
}
