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
            DatatableConfiguration.Components.SearchComponents.Register(typeof(bool), DatatableDefaults.Html.SearchComponents.Boolean);
            DatatableConfiguration.Components.SearchComponents.Register(typeof(bool?), DatatableDefaults.Html.SearchComponents.Boolean);
            DatatableConfiguration.Components.SearchComponents.Register(typeof(DateTime?), DatatableDefaults.Html.SearchComponents.DateTime);
            DatatableConfiguration.Components.SearchComponents.Register(typeof(DateTime), DatatableDefaults.Html.SearchComponents.DateTime);
            DatatableConfiguration.Components.SearchComponents.Default = DatatableDefaults.Html.SearchComponents.Text;

            DatatableConfiguration.TableRenderers.LocalDatatableRenderer = DatatableDefaults.Html.TableRenderers.TwitterBootstrapLocal;
            DatatableConfiguration.TableRenderers.RemoteDatatableRenderer = DatatableDefaults.Html.TableRenderers.TwitterBootstrapRemote;
        }
    }
}