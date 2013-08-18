using System.Collections.Generic;
using System.Web.Mvc;

namespace GenericDatatables.Datatables.Remote.Reply
{
    /// <summary>
    ///     knows how to please the datatable plugin
    /// </summary>
    public class DatatableReply
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the column names, comma separated (used in combination with sName)
        ///     which will allow DataTables to reorder data on the client-side if required for display.
        ///     Note that the number of column names returned must exactly match the number of columns in the table.
        ///     For a more flexible JSON format, please consider using mDataProp.
        /// </summary>
        public string Columns { get; set; }

        /// <summary>
        ///     Gets or sets the data in a 2D array.
        /// </summary>
        public IDictionary<string, string>[] Data { get; set; }

        /// <summary>
        ///     Gets or sets the unaltered copy of sEcho sent from the client side.
        ///     This parameter will change with each draw (it is basically a draw count)
        ///     - so it is important that this is implemented.
        ///     Note that it strongly recommended for security reasons
        ///     that you 'cast' this parameter to an integer in order to prevent Cross Site Scripting (XSS) attacks.
        /// </summary>
        public int Echo { get; set; }

        /// <summary>
        ///     Gets or sets the total records after filtering
        ///     (i.e. the total number of records after filtering has been applied
        ///     - not just the number of records being returned in this result set)
        /// </summary>
        public int TotalDisplayRecords { get; set; }

        /// <summary>
        ///     Gets or sets the total records before filtering (i.e. the total number of records in the database)
        /// </summary>
        public int TotalRecords { get; set; }

        #endregion

        /// <summary>
        ///     Converts a DatatableReply into a datatable accepted format (it's mostly renaming the variables)
        /// </summary>
        /// <returns>
        ///     This datatable reply as a valid Json object
        /// </returns>
        public JsonResult ToJson()
        {
            return new JsonResult
            {
                Data =
                    new
                    {
                        iTotalRecords = TotalRecords,
                        iTotalDisplayRecords = TotalDisplayRecords,
                        sEcho = Echo,
                        sColumns = Columns,
                        aaData = Data
                    },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Echo: {0}, TotalDisplayRecords: {1}, TotalRecords: {2}, Columns: {3}, Data: {4}", Echo, TotalDisplayRecords, TotalRecords, Columns, Data);
        }
    }
}