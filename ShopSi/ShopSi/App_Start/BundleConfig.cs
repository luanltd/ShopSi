using System.Web;
using System.Web.Optimization;

namespace ShopSi
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jsclient").Include(
                      "~/assets/client/js/jquery.min.js",
                      "~/assets/client/js/jquery-ui.js",
                       "~/assets/client/js/bootstrap-3.1.1.min.js",
                        "~/assets/client/js/simpleCart.min.js"
                      ));

            bundles.Add(new StyleBundle("~/bundles/cssclient").Include(
                      "~/assets/client/css/bootstrap.css",
                      "~/assets/client/css/font-awesome.min.css",
                      "~/assets/client/css/bootstrap-social.css",
                      "~/assets/client/css/jquery-ui.css",
                      "~/assets/client/css/style.css",
                      "~/assets/client/css/component.css",
                      "~/assets/client/css/flexslider.css"
                      ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
