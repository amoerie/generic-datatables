using System.Linq;

namespace GenericDatatables.Core.Infrastructure.Including
{
    /// <summary>
    /// Class that is used as a dummy holder that does not change the entities. 
    /// This can be useful in scenario's where an EntityIncluder is passed around through many layers of code
    /// and you want to append inclusions in every layer. 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DummyPropertyInclusionHolder<TEntity>: IPropertyInclusionHolder<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Adds one property inclusion to the <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">The entities</param>
        /// <returns>The entities that are now marked with the property inclusion</returns>
        public IQueryable<TEntity> AddPropertyInclusion(IQueryable<TEntity> entities)
        {
            return entities;
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
