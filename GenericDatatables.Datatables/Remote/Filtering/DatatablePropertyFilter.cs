using System;
using System.Linq.Expressions;
using GenericDatatables.Core.Base.Contracts;
using GenericDatatables.Core.Infrastructure.Filtering;
using LinqKit;

namespace GenericDatatables.Datatables.Remote.Filtering
{
    public class DatatablePropertyFilter<TEntity, TSearch>: IDatatablePropertyFilter<TEntity>
    {
        public Func<string, TSearch> SearchParser { get; set; }
        public Expression<Func<TEntity, TSearch, bool>> SearchFilter { get; set; }

        public IEntityFilter<TEntity> Filter(IEntityFilter<TEntity> filter, string search)
        {
            filter = filter ?? EntityFilter<TEntity>.AsQueryable();
            try
            {
                var parsedSearch = SearchParser(search ?? string.Empty);
                var searchFilter = SearchFilter;
                Expression<Func<TEntity, bool>> predicate = e => searchFilter.Invoke(e, parsedSearch);
                return filter.Where(predicate.Expand());
            }
            catch
            {
                return filter;
            }
        }

        public override string ToString()
        {
            return string.Format("SearchParser: {0}, SearchFilter: {1}", SearchParser, SearchFilter);
        }
    }
}
