using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericDatatables.Core.Infrastructure.Attributes.JetBrains.Annotations;
using GenericDatatables.Datatables.Html.SearchComponents;

namespace GenericDatatables.Datatables.Local.Builder
{
    public interface ILocalDatatableColumnBuilder<TEntity>: ILocalDatatableBuilder<TEntity> where TEntity : class
    {
        ILocalDatatableColumnBuilder<TEntity> Display([NotNull] Func<TEntity, string> display);

        ILocalDatatableColumnBuilder<TEntity> Sortable(bool sortable);

        ILocalDatatableColumnBuilder<TEntity> Searchable(bool searchable);

        ILocalDatatableColumnBuilder<TEntity> Visible(bool visible);

        ILocalDatatableColumnBuilder<TEntity> Width([NotNull] string width);

        ILocalDatatableColumnBuilder<TEntity> Class([NotNull] string @class);

        ILocalDatatableColumnBuilder<TEntity> DefaultContent([NotNull] string defaultContent);

        ILocalDatatableColumnBuilder<TEntity> SearchComponent([NotNull] ISearchComponent searchComponent);
    }
}
