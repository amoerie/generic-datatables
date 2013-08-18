using System.Web.Mvc;

namespace GenericDatatables.Datatables.Extensions
{
    public static class ExtensionsForModelBindingContext
    {
        /// <summary>
        ///     Gets a value from the ModelBindingContext by its key.
        /// </summary>
        /// <param name="bindingContext">
        ///     The binding context.
        /// </param>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the value
        /// </typeparam>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public static T GetValue<T>(this ModelBindingContext bindingContext, string key)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(key);
            if (valueResult == null)
            {
                return default(T);
            }

            return (T) valueResult.ConvertTo(typeof (T));
        }
    }
}
