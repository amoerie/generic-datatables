using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericDatatables.Datatables.Base;

namespace GenericDatatables.Datatables.Local
{
    public interface ILocalDatatableColumn<TEntity, TProperty>: IDatatableColumn<TEntity, TProperty> where TEntity : class
    {
    }
}
