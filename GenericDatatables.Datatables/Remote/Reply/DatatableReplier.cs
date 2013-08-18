using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GenericDatatables.Core.Base.Contracts;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Core.Base.Repositories;
using GenericDatatables.Datatables.Remote.Filtering;
using GenericDatatables.Datatables.Remote.Request;
using GenericDatatables.Datatables.Remote.Sorting;

namespace GenericDatatables.Datatables.Remote.Reply
{
    /// <summary>
    /// Helper class that can parse a <see cref="DatatableReply"/> and give a <see cref="DatatableReply"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DatatableReplier<TEntity> where TEntity : Entity
    {
        private RemoteDatatable<TEntity> Datatable { get; set; }
        private IRepository<TEntity> Repository { get; set; }

        private IEntityIncluder<TEntity> BaseIncluder { get; set; } 
        private IEntityFilter<TEntity> BaseFilter { get; set; }
        private IEntitySorter<TEntity> BaseSorter { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DatatableReplier{TEntity}"/>
        /// </summary>
        /// <param name="datatable">The datatable for which this request was made</param>
        /// <param name="repository">The repository from which the data can be fetched</param>
        /// <param name="baseFilter">The base filter that needs to be applied to the data before the request is parsed</param>
        /// <param name="baseIncluder">The base includer that needs to be applied to the data before the request is parsed</param>
        /// <param name="baseSorter">The base sorter that needs to be applied to the data before the request is parsed</param>
        public DatatableReplier(RemoteDatatable<TEntity> datatable,
                                IRepository<TEntity> repository,
                                IEntityFilter<TEntity> baseFilter = default(IEntityFilter<TEntity>),
                                IEntityIncluder<TEntity> baseIncluder = default(IEntityIncluder<TEntity>),
                                IEntitySorter<TEntity> baseSorter = default(IEntitySorter<TEntity>))
        {
            Datatable = datatable;
            Repository = repository;
            BaseIncluder = baseIncluder;
            BaseFilter = baseFilter;
            BaseSorter = baseSorter;
        }

        /// <summary>
        /// Returns an instance of <see cref="DatatableReply"/> in response to the <paramref name="request"/>
        /// </summary>
        /// <param name="request">The request to respond to</param>
        /// <param name="totalFilter">Filters the total count</param>
        /// <returns>an instance of <see cref="DatatableReply"/> in response to the <paramref name="request"/></returns>
        public JsonResult Reply(DatatableRequest request)
        {
            var filter = new DatatableFilter<TEntity>(BaseFilter, Datatable, request);
            var sorter = new DatatableSorter<TEntity>(BaseSorter, Datatable, request);
            var page = request.DisplayStart/request.DisplayLength;
            var pageSize = request.DisplayLength;
            var totalCount = Repository.Count(BaseFilter);
            var filteredCount = request.ContainsFiltering || BaseFilter != null ? Repository.Count(filter) : totalCount;
            var entities = Repository.List(filter, sorter, page, pageSize, BaseIncluder).ToList();

            var data = new IDictionary<string, string>[entities.Count];
            for (int i = 0; i < entities.Count; i++ )
            {
                var entity = entities[i];
                var dataForEntity = new Dictionary<string, string>();
                foreach (var column in Datatable.Columns)
                {
                    dataForEntity[column.Name] = column.DisplayComponent(entity);
                }
                data[i] = dataForEntity;
            }

            return new DatatableReply
                {
                    Echo = Convert.ToInt32(request.Echo),
                    Columns = string.Join(",", Datatable.Columns.Select(c => c.Name)),
                    Data = data,
                    TotalDisplayRecords = filteredCount,
                    TotalRecords = totalCount
                }.ToJson();
        }
    }
}
