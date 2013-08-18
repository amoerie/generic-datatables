using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using GenericDatatables.Core.Base.Models;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Config;
using GenericDatatables.Datatables.Extensions;
using GenericDatatables.Datatables.Remote.Filtering;
using GenericDatatables.Datatables.Remote.Sorting;
using LinqKit;

namespace GenericDatatables.Datatables.Remote.Builder
{
    public class RemoteDatatableBuilder<TEntity> : DatatableBuilder<TEntity>, IRemoteDatatableBuilder<TEntity> where TEntity : Entity
    {
        private readonly RemoteDatatable<TEntity> _remoteDatatable; 

        public RemoteDatatableBuilder(HtmlHelper htmlHelper, RemoteDatatable<TEntity> datatable) : base(htmlHelper, datatable)
        {
            _remoteDatatable = datatable;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header)
        {
            var column = new RemoteDatatableColumn<TEntity>
            {
                Header = header,
                Name = Guid.NewGuid().ToString(),
                Sortable = false,
                Searchable = false,
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Default
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, bool?>> propertyExpression)
        {
            var propertyFunction = propertyExpression.Compile();
            var column = new RemoteDatatableColumn<TEntity>
            {
                Header = header,
                Name = ExpressionHelper.GetExpressionText(propertyExpression).Replace(".", string.Empty),
                DisplayComponent = entity => Convert.ToString(propertyFunction(entity)),
                PropertyFilter = new DatatablePropertyFilter<TEntity, bool?>
                {
                    SearchParser = search => search == null ? (bool?) null : search == "true",
                    SearchFilter = (entity, search) => propertyExpression.Invoke(entity) == search
                },
                PropertySorters = new List<IDatatablePropertySorter<TEntity>> { new DatatablePropertySorter<TEntity, bool?>(propertyExpression) },
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Lookup<bool?>()
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, int?>> propertyExpression)
        {
            var propertyFunction = propertyExpression.Compile();
            var column = new RemoteDatatableColumn<TEntity>
            {
                Header = header,
                Name = ExpressionHelper.GetExpressionText(propertyExpression).Replace(".", string.Empty),
                DisplayComponent = entity => Convert.ToString(propertyFunction(entity)),
                PropertyFilter = new DatatablePropertyFilter<TEntity, string>
                {
                    SearchParser = search => search,
                    SearchFilter = (entity, search) => SqlFunctions.StringConvert((double?) propertyExpression.Invoke(entity)).ToLower().Contains(search.ToLower())
                },
                PropertySorters = new List<IDatatablePropertySorter<TEntity>> { new DatatablePropertySorter<TEntity, int?>(propertyExpression) },
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Lookup<int?>()
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, double?>> propertyExpression)
        {
            var propertyFunction = propertyExpression.Compile();
            var column = new RemoteDatatableColumn<TEntity>
            {
                Header = header,
                Name = ExpressionHelper.GetExpressionText(propertyExpression).Replace(".", string.Empty),
                DisplayComponent = entity => Convert.ToString(propertyFunction(entity)),
                PropertyFilter = new DatatablePropertyFilter<TEntity, string>
                {
                    SearchParser = search => search,
                    SearchFilter = (entity, search) => SqlFunctions.StringConvert(propertyExpression.Invoke(entity)).ToLower().Contains(search.ToLower())
                },
                PropertySorters = new List<IDatatablePropertySorter<TEntity>> { new DatatablePropertySorter<TEntity, double?>(propertyExpression) },
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Lookup<double?>()
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, decimal?>> propertyExpression)
        {
            var propertyFunction = propertyExpression.Compile();
            var column = new RemoteDatatableColumn<TEntity>
            {
                Header = header,
                Name = ExpressionHelper.GetExpressionText(propertyExpression).Replace(".", string.Empty),
                DisplayComponent = entity => Convert.ToString(propertyFunction(entity)),
                PropertyFilter = new DatatablePropertyFilter<TEntity, string>
                {
                    SearchParser = search => search,
                    SearchFilter = (entity, search) => SqlFunctions.StringConvert(propertyExpression.Invoke(entity)).ToLower().Contains(search.ToLower())
                },
                PropertySorters = new List<IDatatablePropertySorter<TEntity>> { new DatatablePropertySorter<TEntity, decimal?>(propertyExpression) },
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Lookup<decimal?>()
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, long?>> propertyExpression)
        {
            var propertyFunction = propertyExpression.Compile();
            var column = new RemoteDatatableColumn<TEntity>
            {
                Header = header,
                Name = ExpressionHelper.GetExpressionText(propertyExpression).Replace(".", string.Empty),
                DisplayComponent = entity => Convert.ToString(propertyFunction(entity)),
                PropertyFilter = new DatatablePropertyFilter<TEntity, string>
                {
                    SearchParser = search => search,
                    SearchFilter = (entity, search) => SqlFunctions.StringConvert((double?)propertyExpression.Invoke(entity)).ToLower().Contains(search.ToLower())
                },
                PropertySorters = new List<IDatatablePropertySorter<TEntity>> { new DatatablePropertySorter<TEntity, long?>(propertyExpression) },
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Lookup<long?>()
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, short?>> propertyExpression)
        {
            var propertyFunction = propertyExpression.Compile();
            var column = new RemoteDatatableColumn<TEntity>
            {
                Header = header,
                Name = ExpressionHelper.GetExpressionText(propertyExpression).Replace(".", string.Empty),
                DisplayComponent = entity => Convert.ToString(propertyFunction(entity)),
                PropertyFilter = new DatatablePropertyFilter<TEntity, string>
                {
                    SearchParser = search => search,
                    SearchFilter = (entity, search) => SqlFunctions.StringConvert((double?)propertyExpression.Invoke(entity)).ToLower().Contains(search.ToLower())
                },
                PropertySorters = new List<IDatatablePropertySorter<TEntity>> { new DatatablePropertySorter<TEntity, short?>(propertyExpression) },
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Lookup<short?>()
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, string>> propertyExpression)
        {
            var propertyFunction = propertyExpression.Compile();
            var column = new RemoteDatatableColumn<TEntity>
            {
                Header = header,
                Name = ExpressionHelper.GetExpressionText(propertyExpression).Replace(".", string.Empty),
                DisplayComponent = entity => Convert.ToString(propertyFunction(entity)),
                PropertyFilter = new DatatablePropertyFilter<TEntity, string>
                {
                    SearchParser = search => search,
                    SearchFilter = (entity, search) => propertyExpression.Invoke(entity).ToLower().Contains(search.ToLower())
                },
                PropertySorters = new List<IDatatablePropertySorter<TEntity>> { new DatatablePropertySorter<TEntity, string>(propertyExpression) },
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Lookup<string>()
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, DateTime?>> propertyExpression)
        {
            var propertyFunction = propertyExpression.Compile();
            var column = new RemoteDatatableColumn<TEntity>
            {
                Header = header,
                Name = ExpressionHelper.GetExpressionText(propertyExpression).Replace(".", string.Empty),
                DisplayComponent = entity => Convert.ToString(propertyFunction(entity)),
                PropertyFilter = new DatatablePropertyFilter<TEntity, DateTime?>
                {
                    SearchParser = search => string.IsNullOrWhiteSpace(search) ? (DateTime?) null : Convert.ToDateTime(search),
                    SearchFilter = (entity, search) => EntityFunctions.DiffDays(propertyExpression.Invoke(entity), search) == 0
                },
                PropertySorters = new List<IDatatablePropertySorter<TEntity>> { new DatatablePropertySorter<TEntity, DateTime?>(propertyExpression) },
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Lookup<DateTime?>()
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, TimeSpan?>> propertyExpression)
        {
            var propertyFunction = propertyExpression.Compile();
            var column = new RemoteDatatableColumn<TEntity>
            {
                Header = header,
                Name = ExpressionHelper.GetExpressionText(propertyExpression).Replace(".", string.Empty),
                DisplayComponent = entity => Convert.ToString(propertyFunction(entity)),
                PropertyFilter = new DatatablePropertyFilter<TEntity, TimeSpan?>
                {
                    SearchParser = search => string.IsNullOrWhiteSpace(search) ? (TimeSpan?)null : TimeSpan.Parse(search),
                    SearchFilter = (entity, search) => EntityFunctions.DiffSeconds(propertyExpression.Invoke(entity), search) == 0
                },
                PropertySorters = new List<IDatatablePropertySorter<TEntity>> { new DatatablePropertySorter<TEntity, TimeSpan?>(propertyExpression) },
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Lookup<TimeSpan?>()
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column<TProperty>(string header, Expression<Func<TEntity, ICollection<TProperty>>> propertyExpression)
        {
            var column = new RemoteDatatableColumn<TEntity>
            {
                Header = header,
                Name = ExpressionHelper.GetExpressionText(propertyExpression).Replace(".", string.Empty),
                Searchable = false,
                Sortable = false,
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Lookup<ICollection<TProperty>>()
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity>(this, column);
        }

        public override IHtmlString ToHtml(object htmlAttributes)
        {
            var validationResults = _remoteDatatable.Validate().ToList();
            if (validationResults.Any())
            {
                var div = new TagBuilder("div").Class("warning");
                var validationList = new TagBuilder("ul");
                foreach (var validationResult in validationResults)
                    validationList.AppendHtml(new TagBuilder("li").Html(validationResult.Message));
                return div.Html(validationList).ToHtml();
            }

            // Store datatable in storage
            DatatableStorage.Put(_remoteDatatable);

            // Build HTML
            return DatatableConfiguration.Html.Renderers.Table.RemoteDatatableRenderer.Render(HtmlHelper,
                _remoteDatatable,
                htmlAttributes);
        }


    }
}
