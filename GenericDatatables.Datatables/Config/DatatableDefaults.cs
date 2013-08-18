using GenericDatatables.Datatables.Html.Components.DisplayComponents;
using GenericDatatables.Datatables.Html.Renderers.Table;
using GenericDatatables.Datatables.Html.SearchComponents;

namespace GenericDatatables.Datatables.Config
{
    public class DatatableDefaults
    {
        public static class Html
        {
            public static class Components
            {
                public static class SearchComponents
                {
                    private static readonly BooleanSearchComponent BooleanSearchComponent = new BooleanSearchComponent();

                    private static readonly DateTimeSearchComponent DateTimeSearchComponent =
                        new DateTimeSearchComponent();

                    private static readonly TextSearchComponent TextSearchComponent = new TextSearchComponent();

                    public static BooleanSearchComponent Boolean
                    {
                        get { return BooleanSearchComponent; }
                    }

                    public static DateTimeSearchComponent DateTime
                    {
                        get { return DateTimeSearchComponent; }
                    }

                    public static TextSearchComponent Text
                    {
                        get { return TextSearchComponent; }
                    }
                }

                public static class DisplayComponents
                {
                    private static readonly BooleanDisplayComponent BooleanDisplayComponent =
                        new BooleanDisplayComponent();

                    public static BooleanDisplayComponent Boolean { get { return BooleanDisplayComponent; }}
                }
            }

            public static class Renderers
            {
                public static class Table
                {
                    private static readonly TwitterBootstrapRemoteDatatableRenderer
                        TwitterBootstrapRemoteDatatableRenderer = new TwitterBootstrapRemoteDatatableRenderer();

                    private static readonly TwitterBootstrapLocalDatatableRenderer
                        TwitterBootstrapLocalDatatableRenderer = new TwitterBootstrapLocalDatatableRenderer();

                    public static TwitterBootstrapRemoteDatatableRenderer TwitterBootstrapRemote
                    {
                        get { return TwitterBootstrapRemoteDatatableRenderer; }
                    }

                    public static TwitterBootstrapLocalDatatableRenderer TwitterBootstrapLocal
                    {
                        get { return TwitterBootstrapLocalDatatableRenderer; }
                    }
                }
            }
        }
    }
}