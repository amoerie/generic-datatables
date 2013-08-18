using GenericDatatables.Datatables.Config.Html.Components;
using GenericDatatables.Datatables.Html.Renderers.Table;

namespace GenericDatatables.Datatables.Config
{
    public static class DatatableConfiguration
    {
        public static class Html
        {
            public static class Components
            {
                private static readonly SearchComponentRegistry SearchComponentRegistry = new SearchComponentRegistry();
                private static readonly DisplayComponentRegistry DisplayComponentRegistry = new DisplayComponentRegistry();

                public static SearchComponentRegistry SearchComponents { get { return SearchComponentRegistry; } }
                public static DisplayComponentRegistry DisplayComponents { get { return DisplayComponentRegistry; } }

            }

            public static class Renderers
            {
                public static class Table
                {
                    private static IRemoteDatatableRenderer _remoteDatatableRenderer;
                    private static ILocalDatatableRenderer _localDatatableRenderer;

                    public static IRemoteDatatableRenderer RemoteDatatableRenderer
                    {
                        get { return _remoteDatatableRenderer ?? (_remoteDatatableRenderer = DatatableDefaults.Html.Renderers.Table.TwitterBootstrapRemote); }
                        set { _remoteDatatableRenderer = value; }
                    }

                    public static ILocalDatatableRenderer LocalDatatableRenderer
                    {
                        get { return _localDatatableRenderer ?? (_localDatatableRenderer = DatatableDefaults.Html.Renderers.Table.TwitterBootstrapLocal ); }
                        set { _localDatatableRenderer = value; }
                    }
                }
            }
        }
    }
}
