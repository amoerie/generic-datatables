using System.Linq;

namespace GenericDatatables.Core.Infrastructure.Including
{
    public interface IPropertyInclusionHolder<TEntity> where TEntity: class
    {
        /// <summary>
        ///     Adds one property inclusion to the <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">The entities</param>
        /// <returns>The entities that are now marked with the property inclusion</returns>
        IQueryable<TEntity> AddPropertyInclusion(IQueryable<TEntity> entities);
    }
}
