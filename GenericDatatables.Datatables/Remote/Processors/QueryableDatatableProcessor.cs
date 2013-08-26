using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericDatatables.Datatables.Remote.Filtering;
using GenericDatatables.Datatables.Remote.Request;
using GenericDatatables.Datatables.Remote.Sorting;

namespace GenericDatatables.Datatables.Remote.Processors
{
    public class QueryableDatatableProcessor<TEntity>: IDatatableProcessor<TEntity> where TEntity : class
    {
        private IQueryable<TEntity> Entities { get; set; } 

        public QueryableDatatableProcessor(IQueryable<TEntity> entities)
        {
            Entities = entities;
        }

        public IList<TEntity> Process(RemoteDatatable<TEntity> remoteDatatable,
            DatatableRequest datatableRequest,
            out int filteredCount,
            out int totalCount)
        {
            var filter = new DatatableFilter<TEntity>(null, remoteDatatable, datatableRequest);
            var sorter = new DatatableSorter<TEntity>(null, remoteDatatable, datatableRequest);
            var page = datatableRequest.DisplayStart / datatableRequest.DisplayLength;
            var pageSize = datatableRequest.DisplayLength;

            totalCount = Entities.Count();
            var filteredEntities = filter.Filter(Entities);
            filteredCount = datatableRequest.ContainsFiltering ? filteredEntities.Count() : totalCount;

            return sorter.Sort(filteredEntities)
                .Skip(page*pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
