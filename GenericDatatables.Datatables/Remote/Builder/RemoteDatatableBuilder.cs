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
using HtmlBuilders;
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
            var column = new RemoteDatatableColumn<TEntity>(header);
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity, bool?> Column(string header, Expression<Func<TEntity, bool?>> propertyExpression)
        {
            var column = new RemoteDatatableColumn<TEntity, bool?>(header, propertyExpression)
            {
                PropertyFilter = new DatatablePropertyFilter<TEntity, bool?>
                {
                    SearchParser = search => search == null ? (bool?) null : string.Equals(search, "true", StringComparison.InvariantCultureIgnoreCase),
                    SearchFilter = (entity, search) => propertyExpression.Invoke(entity) == search
                },
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity, bool?>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity, int?> Column(string header, Expression<Func<TEntity, int?>> propertyExpression)
        {
            var column = new RemoteDatatableColumn<TEntity, int?>(header, propertyExpression)
            {
                PropertyFilter = new DatatablePropertyFilter<TEntity, string>
                {
                    SearchParser = search => search,
                    SearchFilter = (entity, search) => SqlFunctions.StringConvert((double?) propertyExpression.Invoke(entity)).ToLower().Contains(search.ToLower())
                },
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity, int?>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity, double?> Column(string header, Expression<Func<TEntity, double?>> propertyExpression)
        {
            var column = new RemoteDatatableColumn<TEntity, double?>(header, propertyExpression)
            {
                PropertyFilter = new DatatablePropertyFilter<TEntity, string>
                {
                    SearchParser = search => search,
                    SearchFilter = (entity, search) => SqlFunctions.StringConvert(propertyExpression.Invoke(entity)).ToLower().Contains(search.ToLower())
                },
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity, double?>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity, decimal?> Column(string header, Expression<Func<TEntity, decimal?>> propertyExpression)
        {
            var column = new RemoteDatatableColumn<TEntity, decimal?>(header, propertyExpression)
            {
                PropertyFilter = new DatatablePropertyFilter<TEntity, string>
                {
                    SearchParser = search => search,
                    SearchFilter = (entity, search) => SqlFunctions.StringConvert(propertyExpression.Invoke(entity)).ToLower().Contains(search.ToLower())
                },
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity, decimal?>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity, long?> Column(string header, Expression<Func<TEntity, long?>> propertyExpression)
        {
            var column = new RemoteDatatableColumn<TEntity, long?>(header, propertyExpression)
            {
                PropertyFilter = new DatatablePropertyFilter<TEntity, string>
                {
                    SearchParser = search => search,
                    SearchFilter = (entity, search) => SqlFunctions.StringConvert((double?)propertyExpression.Invoke(entity)).ToLower().Contains(search.ToLower())
                },
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity, long?>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity, short?> Column(string header, Expression<Func<TEntity, short?>> propertyExpression)
        {
            var column = new RemoteDatatableColumn<TEntity, short?>(header, propertyExpression)
            {
                PropertyFilter = new DatatablePropertyFilter<TEntity, string>
                {
                    SearchParser = search => search,
                    SearchFilter = (entity, search) => SqlFunctions.StringConvert((double?)propertyExpression.Invoke(entity)).ToLower().Contains(search.ToLower())
                },
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity, short?>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity, string> Column(string header, Expression<Func<TEntity, string>> propertyExpression)
        {
            var column = new RemoteDatatableColumn<TEntity, string>(header, propertyExpression)
            {
                PropertyFilter = new DatatablePropertyFilter<TEntity, string>
                {
                    SearchParser = search => search,
                    SearchFilter = (entity, search) => propertyExpression.Invoke(entity).ToLower().Contains(search.ToLower())
                },
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity, string>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity, DateTime?> Column(string header, Expression<Func<TEntity, DateTime?>> propertyExpression)
        {
            var column = new RemoteDatatableColumn<TEntity, DateTime?>(header, propertyExpression)
            {
                PropertyFilter = new DatatablePropertyFilter<TEntity, DateTime?>
                {
                    SearchParser = search => string.IsNullOrWhiteSpace(search) ? (DateTime?) null : Convert.ToDateTime(search),
                    SearchFilter = (entity, search) => EntityFunctions.DiffDays(propertyExpression.Invoke(entity), search) == 0
                },
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity, DateTime?>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity, TimeSpan?> Column(string header, Expression<Func<TEntity, TimeSpan?>> propertyExpression)
        {
            var column = new RemoteDatatableColumn<TEntity, TimeSpan?>(header, propertyExpression)
            {
                PropertyFilter = new DatatablePropertyFilter<TEntity, TimeSpan?>
                {
                    SearchParser = search => string.IsNullOrWhiteSpace(search) ? (TimeSpan?)null : TimeSpan.Parse(search),
                    SearchFilter = (entity, search) => EntityFunctions.DiffSeconds(propertyExpression.Invoke(entity), search) == 0
                },
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity, TimeSpan?>(this, column);
        }

        public IRemoteDatatableColumnBuilder<TEntity, ICollection<TProperty>> Column<TProperty>(string header, Expression<Func<TEntity, ICollection<TProperty>>> propertyExpression)
        {
            var column = new RemoteDatatableColumn<TEntity, ICollection<TProperty>>(header, propertyExpression)
            {
                Searchable = false,
                Sortable = false,
            };
            _remoteDatatable.Columns.Add(column);
            return new RemoteDatatableColumnBuilder<TEntity, ICollection<TProperty>>(this, column);
        }

        public override IHtmlString ToHtml(object htmlAttributes)
        {
            var validationResults = _remoteDatatable.Validate().ToList();
            if (validationResults.Any())
            {
                var div = new HtmlTag("div").Class("warning");
                var validationList = new HtmlTag("ul");
                foreach (var validationResult in validationResults)
                    validationList.Append(new HtmlTag("li").Append(validationResult.Message));
                return div.Append(validationList).ToHtml();
            }

            // Store datatable in storage
            DatatableStorage.Put(_remoteDatatable);

            // Build HTML
            return DatatableConfiguration.TableRenderers.RemoteDatatableRenderer.Render(HtmlHelper,
                _remoteDatatable,
                htmlAttributes);
        }


    }
}
