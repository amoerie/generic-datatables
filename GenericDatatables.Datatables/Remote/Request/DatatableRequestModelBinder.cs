using System;
using System.Web.Mvc;
using GenericDatatables.Datatables.Extensions;

namespace GenericDatatables.Datatables.Remote.Request
{
    /// <summary>
    /// A model binder for <see cref="DatatableRequest"/>. Register this model binder in your MVC application if you want to use Datatables.
    /// </summary>
    public class DatatableRequestModelBinder: DefaultModelBinder
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Binds a datatable param object from the binding context
        /// </summary>
        /// <param name="controllerContext">
        ///     The controller context.
        /// </param>
        /// <param name="bindingContext">
        ///     The binding context.
        /// </param>
        /// <returns>
        ///     An instance of a datatable request
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If the controllerContext or bindingContext is null, this method will fail
        /// </exception>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext", "Failed to bind datatables request, controllerContext is null.");
            }

            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext", "Failed to bind datatables request, bindingContext is null.");
            }

            var request = new DatatableRequest
            {
                DatatableId = bindingContext.GetValue<string>("sDatatableId"),
                DisplayStart = bindingContext.GetValue<int>("iDisplayStart"),
                DisplayLength = bindingContext.GetValue<int>("iDisplayLength"),
                ColumnsCount = bindingContext.GetValue<int>("iColumns"),
                GlobalSearch = bindingContext.GetValue<string>("sSearch"),
                SortingColumnsCount = bindingContext.GetValue<int>("iSortingCols"),
                Echo = bindingContext.GetValue<string>("sEcho")
            };

            request.Searchable = new bool[request.ColumnsCount];
            request.Search = new string[request.ColumnsCount];
            request.Regex = new bool[request.ColumnsCount];
            request.Sortable = new bool[request.ColumnsCount];
            request.DataProperties = new string[request.ColumnsCount];
            request.SortingColumns = new int[request.ColumnsCount];
            request.SortDirections = new string[request.ColumnsCount];

            for (int i = 0; i < request.ColumnsCount; i++)
            {
                request.Searchable[i] = bindingContext.GetValue<bool>("bSearchable_" + i);
                request.Search[i] = bindingContext.GetValue<string>("sSearch_" + i);
                request.Regex[i] = bindingContext.GetValue<bool>("bRegex_" + i);
                request.Sortable[i] = bindingContext.GetValue<bool>("bSortable_" + i);
                request.DataProperties[i] = bindingContext.GetValue<string>("mDataProp_" + i);
                request.SortingColumns[i] = bindingContext.GetValue<int>("iSortCol_" + i);
                request.SortDirections[i] = bindingContext.GetValue<string>("sSortDir_" + i);
            }

            return request;
        }

        #endregion
    }
}
