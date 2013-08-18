using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenericDatatables.Datatables;
using GenericDatatables.Datatables.Config;
using GenericDatatables.Datatables.Html.SearchComponents;

namespace GenericDatatables.Web
{
    public class DatatableConfig
    {
        public static void Register()
        {
            DatatableConfiguration.Html.Components.SearchComponents.Register(typeof(bool), DatatableDefaults.Html.Components.SearchComponents.Boolean);
            DatatableConfiguration.Html.Components.SearchComponents.Register(typeof(bool?), DatatableDefaults.Html.Components.SearchComponents.Boolean);
            DatatableConfiguration.Html.Components.SearchComponents.Register(typeof(DateTime?), DatatableDefaults.Html.Components.SearchComponents.DateTime);
            DatatableConfiguration.Html.Components.SearchComponents.Register(typeof(DateTime), DatatableDefaults.Html.Components.SearchComponents.DateTime);
            DatatableConfiguration.Html.Components.SearchComponents.Default = DatatableDefaults.Html.Components.SearchComponents.Text;

            DatatableConfiguration.Html.Renderers.Table.LocalDatatableRenderer = DatatableDefaults.Html.Renderers.Table.TwitterBootstrapLocal;
            DatatableConfiguration.Html.Renderers.Table.RemoteDatatableRenderer = DatatableDefaults.Html.Renderers.Table.TwitterBootstrapRemote;
        }
    }
}