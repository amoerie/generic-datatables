using System.Web.Mvc;

namespace GenericDatatables.Core
{
    /// <summary>
    ///     The datatable reply converter.
    /// </summary>
    public static class DatatableReplyConverter
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Converts a DatatableReply into a datatable accepted format (it's mostly renaming the variables)
        /// </summary>
        /// <param name="reply">
        ///     This datatable reply
        /// </param>
        /// <returns>
        ///     This datatable reply as a valid Json object
        /// </returns>
        public static JsonResult ToJson(this DatatableReply reply)
        {
            return new JsonResult
                {
                    Data =
                        new
                            {
                                iTotalRecords = reply.TotalRecords,
                                iTotalDisplayRecords = reply.TotalDisplayRecords,
                                sEcho = reply.Echo,
                                sColumns = reply.Columns,
                                aaData = reply.Data
                            },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                };
        }

        #endregion
    }
}