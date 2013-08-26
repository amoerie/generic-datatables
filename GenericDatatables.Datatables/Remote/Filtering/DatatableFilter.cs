using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GenericDatatables.Core.Base.Contracts;
using GenericDatatables.Core.Infrastructure.Filtering;
using GenericDatatables.Datatables.Remote.Request;
using LinqKit;

namespace GenericDatatables.Datatables.Remote.Filtering
{
    /// <summary>
    /// Custom filter that implements <see cref="IEntityFilter{TEntity}"/> and provides filtering logic for a <see cref="Datatable"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DatatableFilter<TEntity>: IEntityFilter<TEntity> where TEntity : class 
    {
        private RemoteDatatable<TEntity> Datatable { get; set; }
        private DatatableRequest Request { get; set; }
        private IEntityFilter<TEntity> EntityFilter { get; set; }

        public DatatableFilter(IEntityFilter<TEntity> baseFilter, RemoteDatatable<TEntity> datatable, DatatableRequest request)
        {
            Datatable = datatable;
            Request = request;
            EntityFilter = baseFilter ?? EntityFilter<TEntity>.AsQueryable();
            MakeFilter();
        }

        private void MakeFilter()
        {
            var globalFilter = EntityFilter<TEntity>.AsQueryable();
            // Global search
            if (!string.IsNullOrWhiteSpace(Request.GlobalSearch))
            {
                for (int i = 0; i < Request.DataProperties.Length; i++)
                {
                    if (!Request.Searchable[i])
                        continue;

                    var name = Request.DataProperties[i];
                    var column = Datatable.Columns.Single(c => c.Name.Equals(name));

                    globalFilter = column.Filter(globalFilter, Request.GlobalSearch);
                }

            }

            var propertyFilter = EntityFilter<TEntity>.AsQueryable();
            // Property specific search
            for (int i = 0; i < Request.Search.Length; i++)
            {
                string search = Request.Search[i];
                if (Request.Searchable[i] && !string.IsNullOrWhiteSpace(search))
                {
                    var name = Request.DataProperties[i];
                    var column = Datatable.Columns.Single(c => c.Name.Equals(name));
                    propertyFilter = column.Filter(propertyFilter, search);
                }
            }

            if (globalFilter.Predicates.Any())
            {
                var globalSearchPredicate = globalFilter.Predicates
                    .Aggregate(PredicateBuilder.False<TEntity>(), (accumulate, predicate) => accumulate.Or(predicate)).Expand();
                EntityFilter = EntityFilter.Where(globalSearchPredicate);
            }

            if (propertyFilter.Predicates.Any())
            {
                var propertySpecificPredicate = propertyFilter.Predicates
                    .Aggregate(PredicateBuilder.True<TEntity>(),(accumulate, predicate) => accumulate.And(predicate)).Expand();
                EntityFilter = EntityFilter.Where(propertySpecificPredicate);
            }
        }

        public IQueryable<TEntity> Filter(IQueryable<TEntity> collection)
        {
            return EntityFilter.Filter(collection);
        }

        public IEnumerable<Expression<Func<TEntity, bool>>> Predicates
        {
            get { return EntityFilter.Predicates; }
        }

        public override string ToString()
        {
            return EntityFilter.ToString();
        }
    }
}
