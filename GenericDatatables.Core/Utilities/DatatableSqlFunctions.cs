using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Reflection;

namespace GenericDatatables.Core.Utilities
{
    /// <summary>
    ///     Static class where I keep useful static properties concerning the SqlFunctions class
    /// </summary>
    public static class DatatableSqlFunctions
    {
        #region Static Fields

        /// <summary>
        ///     The date part string.
        /// </summary>
        public static readonly MethodInfo DatePartString = typeof (SqlFunctions).GetMethod(
            "DatePart", new[] {typeof (string), typeof (DateTime?)});

        /// <summary>
        ///     The format dictionary.
        /// </summary>
        public static readonly Dictionary<string, string> FormatDictionary = new Dictionary<string, string>
            {
                {
                    "dd",
                    "day"
                },
                {
                    "MM",
                    "month"
                },
                {
                    "yyyy",
                    "year"
                },
                {
                    "ss",
                    "second"
                },
                {
                    "mm",
                    "minute"
                },
                {
                    "hh",
                    "hour"
                }
            };

        /// <summary>
        ///     The string convert double.
        /// </summary>
        public static readonly MethodInfo StringConvertDouble = typeof (SqlFunctions).GetMethod(
            "StringConvert", new[] {typeof (double?)});

        #endregion
    }
}