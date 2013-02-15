using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using GenericDatatables.Core.Session;

namespace GenericDatatables.Core
{
    /// <summary>
    ///     Builder class that
    ///     - Stores data table properties in the session
    ///     - Builds the necessary html components
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type entity
    /// </typeparam>
    public class DatatableBuilder<TEntity> : IDatatableBuilder<TEntity>
        where TEntity : class
    {
        #region Fields

        /// <summary>
        ///     The datatable id.
        /// </summary>
        private readonly string _datatableId;

        /// <summary>
        ///     The _properties.
        /// </summary>
        private readonly IList<IDatatableProperty<TEntity>> _properties;

        /// <summary>
        ///     The _session.
        /// </summary>
        private readonly HttpSessionStateBase _session;

        /// <summary>
        ///     The _has last column.
        /// </summary>
        private bool _hasLastColumn;

        /// <summary>
        ///     The _last column.
        /// </summary>
        private Func<TEntity, IHtmlString> _lastColumn;

        /// <summary>
        ///     The _last column header.
        /// </summary>
        private string _lastColumnHeader;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatatableBuilder{TEntity}" /> class.
        /// </summary>
        /// <param name="datatableId">
        ///     The datatable id.
        /// </param>
        /// <param name="session">
        ///     The session.
        /// </param>
        public DatatableBuilder(string datatableId, HttpSessionStateBase session)
        {
            _datatableId = datatableId;
            _session = session;
            _properties = new List<IDatatableProperty<TEntity>>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Constructs the HTML of the datatable.
        /// </summary>
        /// <returns>The datatable as HTML</returns>
        public IHtmlString Finish()
        {
            var datatableSessionObject = new DatatableSessionObject<TEntity>
                {
                    DatatableProperties = _properties,
                    HasLastColumn = _hasLastColumn,
                    LastColumn = _lastColumn,
                    LastColumnHeader = _lastColumnHeader
                };
            _session.AddDatatableProperties(_datatableId, datatableSessionObject);
            return BuildHtmlString();
        }

        /// <summary>
        ///     Constructs the HTML of the datatable and also adds
        ///     a last column to the datatable in which you are free to put HTML
        ///     as you like.
        /// </summary>
        /// <param name="lastColumnHeader">
        ///     The text that should appear in the header of the last column
        /// </param>
        /// <param name="lastColumnHtml">
        ///     A lambda function that takes an instance of TEntity and uses it to generate some HTML
        /// </param>
        /// <returns>
        ///     The datatable as HTML.
        /// </returns>
        public IHtmlString LastColumn(string lastColumnHeader, Func<TEntity, IHtmlString> lastColumnHtml)
        {
            _hasLastColumn = true;
            _lastColumn = lastColumnHtml;
            _lastColumnHeader = lastColumnHeader;
            return Finish();
        }

        /// <summary>
        ///     Adds an int property to this datatable
        /// </summary>
        /// <param name="propertyName">The name of the property that will be used to identify this property from the incoming ajax results</param>
        /// <param name="propertyExpression">The expression to get the property from an instance of TEntity</param>
        /// <returns>This datatablebuilder</returns>
        public IDatatableBuilder<TEntity> Property(string propertyName, Expression<Func<TEntity, int?>> propertyExpression)
        {
            _properties.Add(new DatatableProperty<TEntity, int?>(propertyName, propertyExpression));
            return this;
        }

        /// <summary>
        ///     Adds a decimal property to this datatable
        /// </summary>
        /// <param name="propertyName">The name of the property that will be used to identify this property from the incoming ajax results</param>
        /// <param name="propertyExpression">The expression to get the property from an instance of TEntity</param>
        /// <returns>This datatablebuilder</returns>
        public IDatatableBuilder<TEntity> Property(string propertyName, Expression<Func<TEntity, decimal?>> propertyExpression)
        {
            _properties.Add(new DatatableProperty<TEntity, decimal?>(propertyName, propertyExpression));
            return this;
        }

        /// <summary>
        ///     Adds a double property to this datatable
        /// </summary>
        /// <param name="propertyName">The name of the property that will be used to identify this property from the incoming ajax results</param>
        /// <param name="propertyExpression">The expression to get the property from an instance of TEntity</param>
        /// <returns>This datatablebuilder</returns>
        public IDatatableBuilder<TEntity> Property(string propertyName, Expression<Func<TEntity, double?>> propertyExpression)
        {
            _properties.Add(new DatatableProperty<TEntity, double?>(propertyName, propertyExpression));
            return this;
        }

        /// <summary>
        ///     Adds a string property to this datatable
        /// </summary>
        /// <param name="propertyName">The name of the property that will be used to identify this property from the incoming ajax results</param>
        /// <param name="propertyExpression">The expression to get the property from an instance of TEntity</param>
        /// <returns>This datatablebuilder</returns>
        public IDatatableBuilder<TEntity> Property(string propertyName, Expression<Func<TEntity, string>> propertyExpression)
        {
            _properties.Add(new DatatableProperty<TEntity, string>(propertyName, propertyExpression));
            return this;
        }

        /// <summary>
        ///     Adds a datetime property to this datatable
        /// </summary>
        /// <param name="propertyName">The name of the property that will be used to identify this property from the incoming ajax results</param>
        /// <param name="propertyExpression">The expression to get the property from an instance of TEntity</param>
        /// <param name="dateFormat">The format with which to format the date</param>
        /// <returns>This datatablebuilder</returns>
        public IDatatableBuilder<TEntity> Property(string propertyName, Expression<Func<TEntity, DateTime?>> propertyExpression, string dateFormat)
        {
            _properties.Add(new DatatableProperty<TEntity, DateTime?>(propertyName, propertyExpression, dateFormat));
            return this;
        }

        /// <summary>
        ///     Adds a timespan property to this datatable
        /// </summary>
        /// <param name="propertyName">The name of the property that will be used to identify this property from the incoming ajax results</param>
        /// <param name="propertyExpression">The expression to get the property from an instance of TEntity</param>
        /// <param name="timeFormat">The format with which to format the timespan</param>
        /// <returns>This datatablebuilder</returns>
        public IDatatableBuilder<TEntity> Property(string propertyName, Expression<Func<TEntity, TimeSpan?>> propertyExpression, string timeFormat)
        {
            _properties.Add(new DatatableProperty<TEntity, TimeSpan?>(propertyName, propertyExpression, timeFormat));
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Builds the datatable to an HTML string
        /// </summary>
        /// <returns>
        ///     The <see cref="IHtmlString" />.
        /// </returns>
        private IHtmlString BuildHtmlString()
        {
            /**
             * Make new table tag
             */
            var table = new TagBuilder("table");
            table.Attributes["id"] = _datatableId;
            table.Attributes["class"] = "table table-striped table-bordered table-hover";

            /**
             * Build header  (thead)
             */
            var thead = new TagBuilder("thead");
            var theadtr = new TagBuilder("tr");

            foreach (var property in _properties)
            {
                var theadtrth = new TagBuilder("th");
                theadtrth.SetInnerText(property.ColumnHeader);
                theadtr.InnerHtml += theadtrth.ToString();
            }

            if (_hasLastColumn)
            {
                var theadtrth = new TagBuilder("th");
                theadtrth.Attributes["class"] = "datatable-last-column";
                theadtrth.SetInnerText(_lastColumnHeader);
                theadtr.InnerHtml += theadtrth;
            }

            thead.InnerHtml += theadtr.ToString();

            /**
             * Build empty body (tbody)
             */
            var tbody = new TagBuilder("tbody");

            /**
             * Build footer with input elements per property (tfooter
             */
            var tfoot = new TagBuilder("tfoot");
            var tfoottr = new TagBuilder("tr");
            foreach (var property in _properties)
            {
                var tfoottrth = new TagBuilder("th");
                var tfoottrthinput = new TagBuilder("input");
                tfoottrthinput.Attributes["type"] = "text";
                tfoottrthinput.Attributes["name"] = property.ColumnHeader.ToLower().Replace(" ", "_");
                tfoottrthinput.Attributes["placeholder"] = property.ColumnHeader;
                string inputWithIcon = MakeInputWithIcon(tfoottrthinput.ToString());
                tfoottrth.InnerHtml = inputWithIcon;
                tfoottr.InnerHtml += tfoottrth.ToString();
            }

            if (_hasLastColumn)
            {
                tfoottr.InnerHtml += new TagBuilder("th").ToString();
            }

            tfoot.InnerHtml = tfoottr.ToString();

            /**
             * Complete the table
             */
            table.InnerHtml += thead.ToString();
            table.InnerHtml += tbody.ToString();
            table.InnerHtml += tfoot.ToString();

            return MvcHtmlString.Create(table.ToString());
        }

        /// <summary>
        ///     Makes a search box for a property
        /// </summary>
        /// <param name="input">
        ///     The input.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        private string MakeInputWithIcon(string input)
        {
            var div = new TagBuilder("div");
            div.Attributes["class"] = "controls";
            var divdiv = new TagBuilder("div");
            divdiv.Attributes["class"] = "input-prepend";
            var divdivspan = new TagBuilder("span");
            divdivspan.Attributes["class"] = "add-on";
            var divdivspani = new TagBuilder("i");
            divdivspani.Attributes["class"] = "icon-search";
            divdivspan.InnerHtml = divdivspani.ToString();
            divdiv.InnerHtml = divdivspan.ToString();
            divdiv.InnerHtml += input;
            div.InnerHtml = divdiv.ToString();
            return div.ToString();
        }

        #endregion
    }
}