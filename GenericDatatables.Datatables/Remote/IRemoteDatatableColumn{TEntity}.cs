using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GenericDatatables.Core.Base.Contracts;
using GenericDatatables.Core.Infrastructure.Attributes.JetBrains.Annotations;
using GenericDatatables.Datatables.Base;

namespace GenericDatatables.Datatables.Remote
{
    public interface IRemoteDatatableColumn<TEntity>: IDatatableColumn<TEntity> where TEntity : class
    {
        [NotNull]
        IEntitySorter<TEntity> Sort([CanBeNull] IEntitySorter<TEntity> sorter, SortDirection sortDirection);

        [NotNull]
        IEntityFilter<TEntity> Filter([CanBeNull] IEntityFilter<TEntity> filter, string search);
    }
}
