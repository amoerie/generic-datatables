using System;
using System.Linq.Expressions;
using GenericDatatables.Datatables.Html.Components.DisplayComponents;
using GenericDatatables.Datatables.Html.SearchComponents;
using GenericDatatables.Datatables.Html.TableRenderers;

namespace GenericDatatables.Datatables.Config
{
    public static class DatatableDefaults
    {
        public static class Html
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
                private static readonly FontAwesomeBooleanDisplayComponent FontAwesomeBooleanDisplayComponent = new FontAwesomeBooleanDisplayComponent();

                public static FontAwesomeBooleanDisplayComponent FontAwesomeBoolean { get { return FontAwesomeBooleanDisplayComponent; } }
            }

            public static class TableRenderers
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