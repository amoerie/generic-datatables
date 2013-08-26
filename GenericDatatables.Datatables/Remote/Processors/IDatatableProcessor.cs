using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Datatables.Remote.Request;

namespace GenericDatatables.Datatables.Remote.Processors
{
    public interface IDatatableProcessor<TEntity> where TEntity : class
    {
        IList<TEntity> Process(RemoteDatatable<TEntity> remoteDatatable, DatatableRequest datatableRequest, out int filteredCount, out int totalCount);
    }
}
