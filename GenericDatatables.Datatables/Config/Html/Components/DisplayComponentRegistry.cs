using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericDatatables.Datatables.Html.Components.DisplayComponents;

namespace GenericDatatables.Datatables.Config
{
    public class DisplayComponentRegistry
    {
        private readonly IDictionary<Type, IDisplayComponent> _displayComponentDictionary = new Dictionary<Type, IDisplayComponent>();
        private IDisplayComponent _defaultDisplayComponent;

        public IDisplayComponent Default { 
            get { return _defaultDisplayComponent; } 
            set { _defaultDisplayComponent = value; } 
        }

        /// <summary>
        ///     Registers the <paramref name="displayComponent"/> for the given <paramref name="type"/>
        /// </summary>
        /// <typeparam name="TDisplayComponent">The type of the display component</typeparam>
        /// <param name="type">The type</param>
        /// <param name="displayComponent">The display component</param>
        public void Register<TDisplayComponent>(Type type, TDisplayComponent displayComponent)
            where TDisplayComponent : IDisplayComponent
        {
            _displayComponentDictionary.Add(type, displayComponent);
        }

        /// <summary>
        ///     Looks up and returns the display component that was registered for the <typeparamref name="TType"/> type.
        ///     If no such display component was registered, this will return the default display component or throw an exception if no default display component was configured.
        /// </summary>
        /// <typeparam name="TType">The type</typeparam>
        /// <returns>The display component that was registered for the <typeparamref name="TType"/> type.</returns>
        public IDisplayComponent Lookup<TType>()
        {
            IDisplayComponent displayComponent;
            if (_displayComponentDictionary.TryGetValue(typeof(TType), out displayComponent))
                return displayComponent;
            if (_defaultDisplayComponent != null)
                return _defaultDisplayComponent;
            throw new ArgumentException("No display component registration found for type " + typeof(TType));
        }
    }
}
