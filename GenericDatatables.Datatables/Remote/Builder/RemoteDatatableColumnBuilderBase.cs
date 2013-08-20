using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GenericDatatables.Core.Base.Models;

namespace GenericDatatables.Datatables.Remote.Builder
{
    public abstract class RemoteDatatableColumnBuilderBase<TEntity>: IRemoteDatatableBuilder<TEntity> where TEntity : Entity
    {
        private readonly IRemoteDatatableBuilder<TEntity> _remoteDatatableBuilder;

        protected RemoteDatatableColumnBuilderBase(IRemoteDatatableBuilder<TEntity> remoteDatatableBuilder)
        {
            _remoteDatatableBuilder = remoteDatatableBuilder;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header)
        {
            return _remoteDatatableBuilder.Column(header);
        }

        public IRemoteDatatableColumnBuilder<TEntity, bool?> Column(string header, Expression<Func<TEntity, bool?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, int?> Column(string header, Expression<Func<TEntity, int?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, double?> Column(string header, Expression<Func<TEntity, double?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, decimal?> Column(string header, Expression<Func<TEntity, decimal?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, long?> Column(string header, Expression<Func<TEntity, long?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, short?> Column(string header, Expression<Func<TEntity, short?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, string> Column(string header, Expression<Func<TEntity, string>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, DateTime?> Column(string header, Expression<Func<TEntity, DateTime?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, TimeSpan?> Column(string header, Expression<Func<TEntity, TimeSpan?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, ICollection<TProperty>> Column<TProperty>(string header, Expression<Func<TEntity, ICollection<TProperty>>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IHtmlString ToHtml()
        {
            return _remoteDatatableBuilder.ToHtml();
        }

        public IHtmlString ToHtml(object htmlAttributes)
        {
            return _remoteDatatableBuilder.ToHtml(htmlAttributes);
        }
    }
}
