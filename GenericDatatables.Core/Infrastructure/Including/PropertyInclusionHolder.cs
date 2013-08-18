using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GenericDatatables.Core.Infrastructure.Including
{
    public class PropertyInclusionHolder<TEntity, TProperty>: IPropertyInclusionHolder<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, TProperty>> Property { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyInclusionHolder{TEntity,TProperty}"/> class.
        /// </summary>
        public PropertyInclusionHolder(Expression<Func<TEntity, TProperty>> property)
        {
            Property = property;
        }

        /// <summary>
        ///     Adds one property inclusion to the <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">The entities</param>
        /// <returns>The entities that are now marked with the property inclusion</returns>
        public IQueryable<TEntity> AddPropertyInclusion(IQueryable<TEntity> entities)
        {
            return entities.Include(Property);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}", Property.Body);
        }
    }
}
