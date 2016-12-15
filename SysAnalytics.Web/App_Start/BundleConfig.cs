using System.Web;
using System.Web.Optimization;

namespace SysAnalytics.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.min.css"
                      , "~/Content/animate.css"
                      , "~/Content/style.css"
                      //, "~/Content/themes/base/jquery-ui.css"
                      , "~/Content/jquery.jqGrid/ui.multiselect.css"
                      ));


            // Font Awesome icons
            bundles.Add(new StyleBundle("~/fonts/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        //, "~/Scripts/jquery-ui-{version}.js"
                        ));

            // jQueryUI CSS
            bundles.Add(new ScriptBundle("~/Content/jquery-ui").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.css"));

            // jQueryUI 
            bundles.Add(new StyleBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.min.js"
                        ));

            // jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"
                      , "~/Scripts/respond.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                        "~/Scripts/app/inspinia.js"
                      , "~/Scripts/plugins/pace/pace.min.js"
                      , "~/Scripts/plugins/metisMenu/metisMenu.min.js"
                      ));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimScroll/jquery.slimscroll.min.js"));

            //// Inspinia skin config script
            //bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
            //          "~/Scripts/app/skin.config.min.js"));

            // jqGrid styles
            bundles.Add(new StyleBundle("~/Content/jqGrid").Include(
                        "~/Content/plugins/jqGrid/ui.jqgrid.css"
                      ));

            // jqGrid 
            bundles.Add(new ScriptBundle("~/plugins/jqGrid").Include(
                      "~/Scripts/plugins/jqGrid/i18n/grid.locale-en.js"
                    , "~/Scripts/ui.multiselect.js"
                    , "~/Scripts/plugins/jqGrid/jquery.jqGrid.min.js"
                    ));

            //plugins
            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                "~/Scripts/globalize.js",
                "~/Scripts/globalize/number.js"
                ));

            //Custom
            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                "~/Scripts/json.js"
                , "~/Scripts/jqGridExportToExcel.js"
                //, "~/Scripts/jqGridUserIndex.js"
                //, "~/Scripts/jqGridOrderIndex.js"
                ));

            // Awesome bootstrap checkbox
            bundles.Add(new StyleBundle("~/plugins/awesomeCheckboxStyles").Include(
                      "~/Content/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css"));

        }
    }
}
