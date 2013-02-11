using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using GenericDatatables.Core.Utilities;

namespace GenericDatatables.Core.Helper
{
    /// <summary>
    ///     One of the core classes of this project.
    ///     Can convert regular expressions to linq to entity supported expressions by using
    ///     the SqlFunctions methods
    /// </summary>
    internal static class DatatableExpressionsHelper
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Converts a property display expression that points to a property of type double
        ///     into an SQL compatible expression
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of entity
        /// </typeparam>
        /// <param name="displayDoubleExpr">
        ///     the property expression
        /// </param>
        /// <returns>
        ///     The corresponding sql compatible expression
        /// </returns>
        public static Expression<Func<TEntity, string>> ConvertToSqlCompatibleExpression<TEntity>(
            this Expression<Func<TEntity, double?>> displayDoubleExpr)
        {
            Expression displayBody = displayDoubleExpr.Body;
            MethodCallExpression stringConvert = Expression.Call(Methods.SqlFunctions_StringConvert_Double, displayBody);
            return Expression.Lambda(stringConvert, displayDoubleExpr.Parameters) as Expression<Func<TEntity, string>>;
        }

        /// <summary>
        ///     Converts a property display expression that points to a property of type int
        ///     into an SQL compatible expression
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of entity
        /// </typeparam>
        /// <param name="displayIntExpr">
        ///     the property expression
        /// </param>
        /// <returns>
        ///     The corresponding sql compatible expression
        /// </returns>
        public static Expression<Func<TEntity, string>> ConvertToSqlCompatibleExpression<TEntity>(
            this Expression<Func<TEntity, int?>> displayIntExpr)
        {
            Expression displayBody = displayIntExpr.Body;

            // cast int to double
            displayBody = Expression.Convert(displayBody, typeof (double?));
            return
                ConvertToSqlCompatibleExpression(
                    Expression.Lambda(displayBody, displayIntExpr.Parameters) as Expression<Func<TEntity, double?>>);
        }

        /// <summary>
        ///     Converts a property display expression that points to a property of type decimal
        ///     into an SQL compatible expression
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of entity
        /// </typeparam>
        /// <param name="displayDecimalExpr">
        ///     the property expression
        /// </param>
        /// <returns>
        ///     The corresponding sql compatible expression
        /// </returns>
        public static Expression<Func<TEntity, string>> ConvertToSqlCompatibleExpression<TEntity>(
            this Expression<Func<TEntity, decimal?>> displayDecimalExpr)
        {
            Expression displayBody = displayDecimalExpr.Body;
            MethodCallExpression stringConvert = Expression.Call(
                Methods.SqlFunctions_StringConvert_Decimal, displayBody);
            return Expression.Lambda(stringConvert, displayDecimalExpr.Parameters) as Expression<Func<TEntity, string>>;
        }

        /// <summary>
        ///     Converts a property display expression that points to a property of type DateTime
        ///     into an SQL compatible expression
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of entity
        /// </typeparam>
        /// <param name="displayDateTimeExpr">
        ///     the property expression
        /// </param>
        /// <param name="timeSpanFormat">
        ///     the desired datetime format
        /// </param>
        /// <returns>
        ///     The corresponding sql compatible expression
        /// </returns>
        public static Expression<Func<TEntity, string>> ConvertToSqlCompatibleExpression<TEntity>(
            this Expression<Func<TEntity, DateTime?>> displayDateTimeExpr, string timeSpanFormat)
        {
            Expression propertyExpr = displayDateTimeExpr.Body;
            Expression accumulator = Expressions.Constant_Empty;
            Expression body = RecursiveConvert(
                propertyExpr, Methods.SqlFunctions_DatePart_StringNullableDateTime, accumulator, timeSpanFormat);
            LambdaExpression lambda = Expression.Lambda(body, displayDateTimeExpr.Parameters);
            return lambda as Expression<Func<TEntity, string>>;
        }

        /// <summary>
        ///     Converts a property display expression that points to a property of type DateTime
        ///     into an SQL compatible expression
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of entity
        /// </typeparam>
        /// <param name="displayTimeSpanExpr">
        ///     the property expression
        /// </param>
        /// <param name="timeSpanFormat">
        ///     the desired datetime format
        /// </param>
        /// <returns>
        ///     The corresponding sql compatible expression
        /// </returns>
        public static Expression<Func<TEntity, string>> ConvertToSqlCompatibleExpression<TEntity>(
            this Expression<Func<TEntity, TimeSpan?>> displayTimeSpanExpr, string timeSpanFormat)
        {
            Expression propertyExpr = displayTimeSpanExpr.Body;
            Expression accumulator = Expressions.Constant_Empty;
            Expression body = RecursiveConvert(
                propertyExpr, Methods.SqlFunctions_DatePart_StringNullableTimeSpan, accumulator, timeSpanFormat);
            LambdaExpression lambda = Expression.Lambda(body, displayTimeSpanExpr.Parameters);
            return lambda as Expression<Func<TEntity, string>>;
        }

        /// <summary>
        ///     Converts a property expression of any type and calls ToString on the property
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of entity
        /// </typeparam>
        /// <typeparam name="TProperty">
        ///     The type of the property
        /// </typeparam>
        /// <param name="displayExpr">
        ///     The display expression
        /// </param>
        /// <returns>
        ///     The display expression with a ToString() call on the property
        /// </returns>
        public static Expression<Func<TEntity, string>> ConvertToStringExpression<TEntity, TProperty>(
            this Expression<Func<TEntity, TProperty>> displayExpr)
        {
            Expression displayBody = displayExpr.Body;
            MethodCallExpression valueToStringExpression = Expression.Call(
                displayBody, typeof (TProperty).GetMethod("ToString", Type.EmptyTypes));
            return
                Expression.Lambda(valueToStringExpression, displayExpr.Parameters) as Expression<Func<TEntity, string>>;
        }

        /// <summary>
        ///     Converts a property expression of type datetime and calls ToString on the property
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of entity
        /// </typeparam>
        /// <param name="displayExpr">
        ///     The display expression
        /// </param>
        /// <param name="timeSpanFormat">
        ///     The desired datetime format
        /// </param>
        /// <returns>
        ///     The display expression with a ToString() call on the property
        /// </returns>
        public static Expression<Func<TEntity, string>> ConvertToStringExpression<TEntity>(
            this Expression<Func<TEntity, DateTime?>> displayExpr, string timeSpanFormat)
        {
            MethodCallExpression hasValueExpression = Expression.Call(
                displayExpr.Body, Methods.NullableDateTime_HasValue);
            MethodCallExpression valueExpression = Expression.Call(displayExpr.Body, Methods.NullableDateTime_Value);
            MethodCallExpression valueToStringExpression = Expression.Call(
                valueExpression,
                Methods.DateTime_ToString_String,
                new Expression[] {Expression.Constant(timeSpanFormat)});
            ConstantExpression emptyExpression = Expression.Constant(string.Empty);
            ConditionalExpression valueOrEmptyExpression = Expression.Condition(
                hasValueExpression, valueToStringExpression, emptyExpression, typeof (string));
            var result =
                Expression.Lambda(valueOrEmptyExpression, displayExpr.Parameters) as Expression<Func<TEntity, string>>;
            return result;
        }

        /// <summary>
        ///     Converts a property expression of type timespan and calls ToString on the property
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The type of entity
        /// </typeparam>
        /// <param name="displayTimeSpanExpr">
        ///     The display expression
        /// </param>
        /// <param name="timeSpanFormat">
        ///     The desired timespan format
        /// </param>
        /// <returns>
        ///     The display expression with a ToString() call on the property
        /// </returns>
        public static Expression<Func<TEntity, string>> ConvertToStringExpression<TEntity>(
            this Expression<Func<TEntity, TimeSpan?>> displayTimeSpanExpr, string timeSpanFormat)
        {
            MethodCallExpression hasValueExpression = Expression.Call(
                displayTimeSpanExpr.Body, Methods.NullableTimeSpan_HasValue);
            MethodCallExpression valueExpression = Expression.Call(
                displayTimeSpanExpr.Body, Methods.NullableTimeSpan_Value);
            MethodCallExpression valueToStringExpressoin = Expression.Call(
                valueExpression,
                Methods.TimeSpan_ToString_String,
                new Expression[] {Expression.Constant(timeSpanFormat)});
            ConstantExpression stringEmptyExpression = Expression.Constant(string.Empty);
            ConditionalExpression valueOrEmptyExpression = Expression.Condition(
                hasValueExpression, valueToStringExpressoin, stringEmptyExpression, typeof (string));
            var result =
                Expression.Lambda(valueOrEmptyExpression, displayTimeSpanExpr.Parameters) as
                Expression<Func<TEntity, string>>;
            return result;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Recursively runs through a dateformat and uses the SqlFunctions.Datepart method to parse formatting to values.
        ///     Example of recursion:
        ///     1. RecursiveConvert(e =&gt; e.MyProperty, SqlFunctions_DatePart_StringNullableDateTime, Expressions.Constant_Empty, "dd/MM/yyyy")
        ///     2. RecursiveConvert(e =&gt; e.MyProperty, SqlFunctions.DatePart_StringNullableDateTime, Concat(Expressions.Constant_Empty, SqlFunctions.Datepart("day", e.MyProperty)), "/MM/yyyy)
        ///     3. RecursiveConvert(e =&gt; e.MyProperty, SqlFunctions.DatePart_StringNullableDateTime, Concat(Concat(Expressions.Constant_Empty, SqlFunctions.Datepart("day", e.MyProperty)), "/"), "MM/yyyy)
        ///     etc.
        /// </summary>
        /// <param name="propertyExpr">
        ///     The property expression
        /// </param>
        /// <param name="datePartMethod">
        ///     DatePart method for Timespan or Datetime
        /// </param>
        /// <param name="accumulator">
        ///     The recursive accumulator
        /// </param>
        /// <param name="remainingFormat">
        ///     The remaining datetime format
        /// </param>
        /// <returns>
        ///     An expression that contains recursively wrapped calls to SqlFunctions.DatePart
        /// </returns>
        private static Expression RecursiveConvert(
            Expression propertyExpr, MethodInfo datePartMethod, Expression accumulator, string remainingFormat)
        {
            if (string.IsNullOrWhiteSpace(remainingFormat))
            {
                return accumulator;
            }

            if (DatatableSqlFunctions.FormatDictionary.Any(pair => remainingFormat.StartsWith(pair.Key)))
            {
                KeyValuePair<string, string> format =
                    DatatableSqlFunctions.FormatDictionary.First(pair => remainingFormat.StartsWith(pair.Key));
                ConstantExpression valueExpr = Expression.Constant(format.Value);
                MethodCallExpression dateNameExpr = Expression.Call(datePartMethod, valueExpr, propertyExpr);
                UnaryExpression convertToDoubleExpr = Expression.Convert(dateNameExpr, typeof (double?));
                MethodCallExpression convertToStringExpr = Expression.Call(
                    DatatableSqlFunctions.StringConvertDouble, convertToDoubleExpr);
                MethodCallExpression replaceSpacesWithEmptyExpr = Expression.Call(
                    convertToStringExpr,
                    Methods.String_Replace_StringString,
                    new[] {Expressions.Constant_Space, Expressions.Constant_Empty});
                MethodCallExpression concatWithAccumulatorExpr = Expression.Call(
                    Methods.String_Concat_StringString, accumulator, replaceSpacesWithEmptyExpr);
                return RecursiveConvert(
                    propertyExpr,
                    datePartMethod,
                    concatWithAccumulatorExpr,
                    remainingFormat.Substring(format.Key.Length));
            }

            ConstantExpression prefix = Expression.Constant(remainingFormat.Substring(0, 1));
            MethodCallExpression concat = Expression.Call(Methods.String_Concat_StringString, accumulator, prefix);
            return RecursiveConvert(propertyExpr, datePartMethod, concat, remainingFormat.Substring(1));
        }

        #endregion

        // ReSharper disable InconsistentNaming

        /// <summary>
        ///     The expressions.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore",
            Justification = "Reviewed. Suppression is OK here.")]
        private static class Expressions
        {
            #region Static Fields

            /// <summary>
            ///     The constant_ empty.
            /// </summary>
            internal static readonly Expression Constant_Empty = Expression.Constant(string.Empty);

            /// <summary>
            ///     The constant_ space.
            /// </summary>
            internal static readonly Expression Constant_Space = Expression.Constant(" ");

            #endregion
        }

        /// <summary>
        ///     The methods.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore",
            Justification = "Reviewed. Suppression is OK here.")]
        private static class Methods
        {
            #region Static Fields

            /// <summary>
            ///     The date time_ to string_ string.
            /// </summary>
            internal static readonly MethodInfo DateTime_ToString_String = typeof (DateTime).GetMethod(
                "ToString", new[] {typeof (string)});

            /// <summary>
            ///     The nullable date time_ has value.
            /// </summary>
            internal static readonly MethodInfo NullableDateTime_HasValue =
                typeof (DateTime?).GetProperty("HasValue").GetGetMethod();

            /// <summary>
            ///     The nullable date time_ value.
            /// </summary>
            internal static readonly MethodInfo NullableDateTime_Value =
                typeof (DateTime?).GetProperty("Value").GetGetMethod();

            /// <summary>
            ///     The nullable time span_ has value.
            /// </summary>
            internal static readonly MethodInfo NullableTimeSpan_HasValue =
                typeof (TimeSpan?).GetProperty("HasValue").GetGetMethod();

            /// <summary>
            ///     The nullable time span_ value.
            /// </summary>
            internal static readonly MethodInfo NullableTimeSpan_Value =
                typeof (TimeSpan?).GetProperty("Value").GetGetMethod();

            /// <summary>
            ///     The sql functions_ date part_ string nullable date time.
            /// </summary>
            internal static readonly MethodInfo SqlFunctions_DatePart_StringNullableDateTime =
                typeof (SqlFunctions).GetMethod("DatePart", new[] {typeof (string), typeof (DateTime?)});

            /// <summary>
            ///     The sql functions_ date part_ string nullable time span.
            /// </summary>
            internal static readonly MethodInfo SqlFunctions_DatePart_StringNullableTimeSpan =
                typeof (SqlFunctions).GetMethod("DatePart", new[] {typeof (string), typeof (TimeSpan?)});

            /// <summary>
            ///     The sql functions_ string convert_ decimal.
            /// </summary>
            internal static readonly MethodInfo SqlFunctions_StringConvert_Decimal =
                typeof (SqlFunctions).GetMethod("StringConvert", new[] {typeof (decimal?)});

            /// <summary>
            ///     The sql functions_ string convert_ double.
            /// </summary>
            internal static readonly MethodInfo SqlFunctions_StringConvert_Double =
                typeof (SqlFunctions).GetMethod("StringConvert", new[] {typeof (double?)});

            /// <summary>
            ///     The string_ concat_ string string.
            /// </summary>
            internal static readonly MethodInfo String_Concat_StringString = typeof (string).GetMethod(
                "Concat", new[] {typeof (string), typeof (string)});

            /// <summary>
            ///     The string_ replace_ string string.
            /// </summary>
            internal static readonly MethodInfo String_Replace_StringString = typeof (string).GetMethod(
                "Replace", new[] {typeof (string), typeof (string)});

            /// <summary>
            ///     The time span_ to string_ string.
            /// </summary>
            internal static readonly MethodInfo TimeSpan_ToString_String = typeof (TimeSpan).GetMethod(
                "ToString", new[] {typeof (string)});

            #endregion

            // variable names are Class_Method_Parameters
        }
    }
}