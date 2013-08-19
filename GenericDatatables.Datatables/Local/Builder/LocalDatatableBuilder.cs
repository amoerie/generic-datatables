using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using GenericDatatables.Datatables.Base;
using GenericDatatables.Datatables.Config;

namespace GenericDatatables.Datatables.Local.Builder
{
    public class LocalDatatableBuilder<TEntity> : DatatableBuilder<TEntity>, ILocalDatatableBuilder<TEntity> where TEntity : class
    {
        private readonly LocalDatatable<TEntity> _localDatatable;

        public LocalDatatableBuilder(HtmlHelper htmlHelper, LocalDatatable<TEntity> datatable)
            : base(htmlHelper, datatable)
        {
            _localDatatable = datatable;
        }

        public override IHtmlString ToHtml(object htmlAttributes)
        {
            return DatatableConfiguration.TableRenderers.LocalDatatableRenderer.Render(HtmlHelper,_localDatatable,htmlAttributes);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header)
        {
            var column = new LocalDatatableColumn<TEntity>(header);
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity, bool?> Column(string header, Expression<Func<TEntity, bool?>> propertyExpression)
        {
            var column = new LocalDatatableColumn<TEntity, bool?>(header, propertyExpression);
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity, bool?>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity, int?> Column(string header, Expression<Func<TEntity, int?>> propertyExpression)
        {
            var column = new LocalDatatableColumn<TEntity, int?>(header, propertyExpression);
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity, int?>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity, double?> Column(string header, Expression<Func<TEntity, double?>> propertyExpression)
        {
            var column = new LocalDatatableColumn<TEntity, double?>(header, propertyExpression);
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity, double?>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity, decimal?> Column(string header, Expression<Func<TEntity, decimal?>> propertyExpression)
        {
            var column = new LocalDatatableColumn<TEntity, decimal?>(header, propertyExpression);
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity, decimal?>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity, long?> Column(string header, Expression<Func<TEntity, long?>> propertyExpression)
        {
            var column = new LocalDatatableColumn<TEntity, long?>(header, propertyExpression);
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity, long?>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity, short?> Column(string header, Expression<Func<TEntity, short?>> propertyExpression)
        {
            var column = new LocalDatatableColumn<TEntity, short?>(header, propertyExpression);
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity, short?>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity, string> Column(string header, Expression<Func<TEntity, string>> propertyExpression)
        {
            var column = new LocalDatatableColumn<TEntity, string>(header, propertyExpression);
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity, string>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity, DateTime?> Column(string header, Expression<Func<TEntity, DateTime?>> propertyExpression)
        {
            var column = new LocalDatatableColumn<TEntity, DateTime?>(header, propertyExpression);
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity, DateTime?>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity, TimeSpan?> Column(string header, Expression<Func<TEntity, TimeSpan?>> propertyExpression)
        {
            var column = new LocalDatatableColumn<TEntity, TimeSpan?>(header, propertyExpression);
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity, TimeSpan?>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity, ICollection<TProperty>> Column <TProperty>(string header, Expression<Func<TEntity, ICollection<TProperty>>> propertyExpression)
        {
            var column = new LocalDatatableColumn<TEntity, ICollection<TProperty>>(header, propertyExpression);
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity, ICollection<TProperty>>(this, column);
        }
    }
}