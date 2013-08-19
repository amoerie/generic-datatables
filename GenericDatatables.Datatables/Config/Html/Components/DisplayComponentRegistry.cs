using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using GenericDatatables.Datatables.Html.Components.DisplayComponents;

namespace GenericDatatables.Datatables.Config
{
    public class DisplayComponentRegistry
    {
        private readonly IDictionary<Type, object> _displayComponentDictionary = new Dictionary<Type, object>();

        public void Register<TType>(IDisplayComponent<TType> displayComponent)
        {
            _displayComponentDictionary[typeof (TType)] = displayComponent;
        }

        /// <summary>
        ///     Looks up and returns the display component that was registered for the <typeparamref name="TType"/> type.
        ///     If no such display component was registered, this will return null
        /// </summary>
        /// <typeparam name="TType">The type</typeparam>
        /// <returns>The display component that was registered for the <typeparamref name="TType"/> type.</returns>
        public IDisplayComponent<TType> Lookup<TType>()
        {
            object displayComponent;
            if (_displayComponentDictionary.TryGetValue(typeof(TType), out displayComponent))
                return (IDisplayComponent<TType>) displayComponent;
            return null;
        }
    }
}
