using System;
using System.Linq.Expressions;
using GenericDatatables.Datatables.Config.Html.Components;
using GenericDatatables.Datatables.Html.Components.DisplayComponents;
using GenericDatatables.Datatables.Html.TableRenderers;

namespace GenericDatatables.Datatables.Config
{
    public static class DatatableConfiguration
    {
        public static class Components
        {
            private static readonly SearchComponentRegistry SearchComponentRegistry = new SearchComponentRegistry();
            private static readonly DisplayComponentRegistry DisplayComponentRegistry = new DisplayComponentRegistry();

            public static SearchComponentRegistry SearchComponents { get { return SearchComponentRegistry; } }
            public static DisplayComponentRegistry DisplayComponents { get { return DisplayComponentRegistry; } }
        }

        public static class TableRenderers
        {
            private static IRemoteDatatableRenderer _remoteDatatableRenderer;
            private static ILocalDatatableRenderer _localDatatableRenderer;

            public static IRemoteDatatableRenderer RemoteDatatableRenderer
            {
                get { return _remoteDatatableRenderer ?? (_remoteDatatableRenderer = DatatableDefaults.Html.TableRenderers.TwitterBootstrapRemote); }
                set { _remoteDatatableRenderer = value; }
            }

            public static ILocalDatatableRenderer LocalDatatableRenderer
            {
                get { return _localDatatableRenderer ?? (_localDatatableRenderer = DatatableDefaults.Html.TableRenderers.TwitterBootstrapLocal); }
                set { _localDatatableRenderer = value; }
            }
        }
    }
}
