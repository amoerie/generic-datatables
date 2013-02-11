namespace GenericDatatables.Core
{
    /// <summary>
    ///     DTO object that is used to catch the datatables ajax request
    /// </summary>
    public class DatatableParam
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the number of columns being displayed (useful for getting individual column search info)
        /// </summary>
        public int ColumnsCount { get; set; }

        /// <summary>
        ///     Gets or sets the value specified by mDataProp for each column.
        ///     This can be useful for ensuring that the processing of data is independent from the order of the columns.
        /// </summary>
        public string[] DataProperties { get; set; }

        /// <summary>
        ///     Gets or sets the custom property to be able to keep property information
        ///     on the server-side in session
        /// </summary>
        public string DatatableId { get; set; }

        /// <summary>
        ///     Gets or sets the number of records that the table can display in the current draw.
        ///     It is expected that the number of records returned will be equal to this number,
        ///     unless the server has fewer records to return.
        /// </summary>
        public int DisplayLength { get; set; }

        /// <summary>
        ///     Gets or sets the display start point in the current data set.
        /// </summary>
        public int DisplayStart { get; set; }

        /// <summary>
        ///     Gets or sets the information for DataTables to use for rendering.
        /// </summary>
        public string Echo { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the global filter should be treated as a regular expression for advanced filtering or not.
        /// </summary>
        public bool GlobalRegex { get; set; }

        /// <summary>
        ///     Gets or sets the global search field
        /// </summary>
        public string GlobalSearch { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the individual column filter should be treated as a regular expression for advanced filtering or not
        /// </summary>
        public bool[] Regex { get; set; }

        /// <summary>
        ///     Gets or sets the individual column filter
        /// </summary>
        public string[] Search { get; set; }

        /// <summary>
        ///     Gets or sets the indicator for if a column is flagged as searchable or not on the client-side
        /// </summary>
        public bool[] Searchable { get; set; }

        /// <summary>
        ///     Gets or sets the direction to be sorted - "desc" or "asc".
        /// </summary>
        public string[] SortDirections { get; set; }

        /// <summary>
        ///     Gets or sets the indicator for if a column is flagged as sortable or not on the client-side
        /// </summary>
        public bool[] Sortable { get; set; }

        /// <summary>
        ///     Gets or sets the column being sorted on (you will need to decode this number for your database)
        /// </summary>
        public int[] SortingColumns { get; set; }

        /// <summary>
        ///     Gets or sets the number of columns to sort on
        /// </summary>
        public int SortingColumnsCount { get; set; }

        #endregion
    }
}