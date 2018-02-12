using System.Web;
using System.Web.Optimization;

namespace GiftShop1
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include(
                    "~/Scripts/angular.min.js",
                    "~/Scripts/ui-bootstrap-tpls-2.5.0.min.js",
                    "~/Scripts/angular-route.min.js")
                .Include(
                    "~/Scripts/app/app.module.js", //Main angular module
                    "~/Scripts/app/app.config.js") //Angular Configuration 
                .IncludeDirectory("~/Scripts/app", "*module.js", true)); //Modules loaded

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/numeral.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/alerts").Include(
                      "~/Scripts/sweetalert.min.js",
                      "~/Scripts/notify.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/icons").Include(
                      "~/Content/font-awesome.min.css"));
        }
    }
}
