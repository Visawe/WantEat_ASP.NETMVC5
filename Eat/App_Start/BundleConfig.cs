using System.Web;
using System.Web.Optimization;

namespace Eat
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/myScript").Include(
                      "~/Scripts/myScript/app.js",
                      "~/Scripts/myScript/dashboard2.js",
                      "~/Scripts/myScript/demo.js",
                       "~/Scripts/myUserScript/slick.js"));

            bundles.Add(new ScriptBundle("~/bundles/myAdminRestScript").Include(
                       "~/Scripts/myUserScript/custom.js",
                       "~/Scripts/myUserScript/nouislider.js"));

            bundles.Add(new ScriptBundle("~/bundles/ContentMyAdminRest/css").Include(
                       "~/Content/UserStyle/nouislider.css",
                        "~/Content/UserStyle/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/myUserScript").Include(
                      "~/Scripts/myUserScript/custom.js",
                      //"~/Scripts/myUserScript/nouislider.js",
                      "~/Scripts/myUserScript/slick.js",
                      "~/Scripts/myUserScript/ownScript.js",
                      "~/Scripts/myUserScript/jquery.fancybox.pack.js",
                      "~/Scripts/myUserScript/jquery.mixitup.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/AdminLTE.css",
                       "~/Content/Site.css",
                      "~/Content/skin-green.css"));

            bundles.Add(new StyleBundle("~/ContentUser/css").Include(
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/UserStyle/style.css",
                      "~/Content/UserStyle/green-theme.css",
                      "~/Content/UserStyle/slick.css",
                      //"~/Content/UserStyle/nouislider.css",
                      "~/Content/Site.css",
                      "~/Content/themes/base/all.css",
                      "~/Content/UserStyle/jquery.fancybox.css"
                      ));
        }
    }
}
