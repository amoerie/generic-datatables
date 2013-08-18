using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericDatatables.Datatables.Html.SearchComponents;

namespace GenericDatatables.Datatables.Config.Html.Components
{
    public class SearchComponentRegistry
    {
        private readonly IDictionary<Type, ISearchComponent> _searchComponentDictionary = new Dictionary<Type, ISearchComponent>();
        private ISearchComponent _defaultSearchComponent;

        public ISearchComponent Default
        {
            get { return _defaultSearchComponent; }
            set { _defaultSearchComponent = value; }
        }

        /// <summary>
        ///     Registers the <paramref name="searchComponent"/> for the given <paramref name="type"/>
        /// </summary>
        /// <typeparam name="TSearchComponent">The type of the search component</typeparam>
        /// <param name="type">The type</param>
        /// <param name="searchComponent">The search component</param>
        public void Register<TSearchComponent>(Type type, TSearchComponent searchComponent)
            where TSearchComponent : ISearchComponent
        {
            _searchComponentDictionary.Add(type, searchComponent);
        }

        /// <summary>
        ///     Looks up and returns the search component that was registered for the <typeparamref name="TType"/> type.
        ///     If no such search component was registered, this will return the default search component or throw an exception if no default search component was configured.
        /// </summary>
        /// <typeparam name="TType">The type</typeparam>
        /// <returns>The search component that was registered for the <typeparamref name="TType"/> type.</returns>
        public ISearchComponent Lookup<TType>()
        {
            ISearchComponent searchComponent;
            if (_searchComponentDictionary.TryGetValue(typeof(TType), out searchComponent))
                return searchComponent;
            if (_defaultSearchComponent != null)
                return _defaultSearchComponent;
            throw new ArgumentException("No search component registration found for type " + typeof(TType));
        }
    }
}
