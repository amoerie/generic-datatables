using System;
using System.Web.Mvc;
using GenericDatatables.Core.Helper;

namespace GenericDatatables.Core
{
    /// <summary>
    ///     Should probably be called "DatatableParamModelBinder" but I already had a DatatableReplyConverter
    ///     and this was the consistent decision...
    ///     You know, I'm the programmer! I can do what I want! Who are you to tell me how to write my code!?
    /// </summary>
    public class DatatableParamConverter : DefaultModelBinder
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
        ///     An instance of datatable param
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If the controllerContext or bindingContext is null, this method will fail
        /// </exception>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext", "controllerContext is null.");
            }

            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext", "bindingContext is null.");
            }

            var param = new DatatableParam
                {
                    DatatableId = bindingContext.GetValue<string>("datatableId"),
                    DisplayStart = bindingContext.GetValue<int>("iDisplayStart"),
                    DisplayLength = bindingContext.GetValue<int>("iDisplayLength"),
                    ColumnsCount = bindingContext.GetValue<int>("iColumns"),
                    GlobalSearch = bindingContext.GetValue<string>("sSearch"),
                    SortingColumnsCount = bindingContext.GetValue<int>("iSortingCols"),
                    Echo = bindingContext.GetValue<string>("sEcho")
                };

            param.Searchable = new bool[param.ColumnsCount];
            param.Search = new string[param.ColumnsCount];
            param.Regex = new bool[param.ColumnsCount];
            param.Sortable = new bool[param.ColumnsCount];
            param.DataProperties = new string[param.ColumnsCount];
            param.SortingColumns = new int[param.ColumnsCount];
            param.SortDirections = new string[param.ColumnsCount];

            for (int i = 0; i < param.ColumnsCount; i++)
            {
                param.Searchable[i] = bindingContext.GetValue<bool>("bSearchable_" + i);
                param.Search[i] = bindingContext.GetValue<string>("sSearch_" + i);
                param.Regex[i] = bindingContext.GetValue<bool>("bRegex_" + i);
                param.Sortable[i] = bindingContext.GetValue<bool>("bSortable_" + i);
                param.DataProperties[i] = bindingContext.GetValue<string>("mDataProp_" + i);
                param.SortingColumns[i] = bindingContext.GetValue<int>("iSortCol_" + i);
                param.SortDirections[i] = bindingContext.GetValue<string>("sSortDir_" + i);
            }

            return param;
        }

        #endregion
    }
}