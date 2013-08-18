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
            return DatatableConfiguration.Html.Renderers.Table.LocalDatatableRenderer.Render(HtmlHelper,
                _localDatatable,
                htmlAttributes);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header)
        {
            var column = new LocalDatatableColumn<TEntity>
            {
                Header = header,
                Name = Guid.NewGuid().ToString(),
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Default
            };
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, bool?>> propertyExpression)
        {
            var column = new LocalDatatableColumn<TEntity>
            {
                Header = header,
                Name = Guid.NewGuid().ToString(),
                DisplayComponent = DatatableConfiguration.Html.Components.DisplayComponents.Lookup<bool?>(),
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Lookup<bool?>()
            };
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, int?>> propertyExpression)
        {
            var propertyFunction = propertyExpression.Compile();
            var column = new LocalDatatableColumn<TEntity>
            {
                Header = header,
                Name = ExpressionHelper.GetExpressionText(propertyExpression),
                DisplayComponent = DatatableConfiguration.Html.Components.DisplayComponents.Lookup<int?>(),
                SearchComponent = DatatableConfiguration.Html.Components.SearchComponents.Lookup<int?>()
            };
            _localDatatable.Columns.Add(column);
            return new LocalDatatableColumnBuilder<TEntity>(this, column);
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, double?>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, decimal?>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, long?>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, short?>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, string>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, DateTime?>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        public ILocalDatatableColumnBuilder<TEntity> Column(string header, Expression<Func<TEntity, TimeSpan?>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        public ILocalDatatableColumnBuilder<TEntity> Column <TProperty>(string header, Expression<Func<TEntity, ICollection<TProperty>>> propertyExpression)
        {
            throw new NotImplementedException();
        }
    }
}