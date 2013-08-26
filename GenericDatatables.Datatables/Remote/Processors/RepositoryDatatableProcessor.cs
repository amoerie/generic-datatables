using System.Collections.Generic;
using System.Linq;
using GenericDatatables.Core.Base.Contracts;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Core.Base.Repositories;
using GenericDatatables.Datatables.Remote.Filtering;
using GenericDatatables.Datatables.Remote.Request;
using GenericDatatables.Datatables.Remote.Sorting;

namespace GenericDatatables.Datatables.Remote.Processors
{
    public class RepositoryDatatableProcessor<TEntity>: IDatatableProcessor<TEntity> where TEntity : Entity
    {
        private IRepository<TEntity> Repository { get; set; }
        private IEntityFilter<TEntity> BaseFilter { get; set; }
        private IEntitySorter<TEntity> BaseSorter { get; set; }
        private IEntityIncluder<TEntity> BaseIncluder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository">The repository from which the data can be fetched</param>
        /// <param name="baseFilter">The base filter that needs to be applied to the data before the request is parsed</param>
        /// <param name="baseIncluder">The base includer that needs to be applied to the data before the request is parsed</param>
        /// <param name="baseSorter">The base sorter that needs to be applied to the data before the request is parsed</param>
        public RepositoryDatatableProcessor(
            IRepository<TEntity> repository, 
            IEntityFilter<TEntity> baseFilter = null,
            IEntitySorter<TEntity> baseSorter = null,
            IEntityIncluder<TEntity> baseIncluder = null)
        {
            Repository = repository;
            BaseFilter = baseFilter;
            BaseSorter = baseSorter;
            BaseIncluder = baseIncluder;
        }

        public IList<TEntity> Process(RemoteDatatable<TEntity> remoteDatatable, DatatableRequest datatableRequest, out int filteredCount, out int totalCount)
        {
            var filter = new DatatableFilter<TEntity>(BaseFilter, remoteDatatable, datatableRequest);
            var sorter = new DatatableSorter<TEntity>(BaseSorter, remoteDatatable, datatableRequest);
            var page = datatableRequest.DisplayStart / datatableRequest.DisplayLength;
            var pageSize = datatableRequest.DisplayLength;
            totalCount = Repository.Count(BaseFilter);
            filteredCount = datatableRequest.ContainsFiltering || BaseFilter != null ? Repository.Count(filter) : totalCount;
            return Repository.List(filter, sorter, page, pageSize, BaseIncluder).ToList();
        }
    }
}
