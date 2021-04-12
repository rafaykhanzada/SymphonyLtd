using System.Web;
using System.Web.Optimization;

namespace SymphonyLtd
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            //Custom Bundling Admin Side
            bundles.Add(new StyleBundle("~/Admin/css").Include(
                    "~/App_Assets/Admin/css/app.min.css",
                    "~/App_Assets/Admin/css/style.css",
                    "~/App_Assets/Admin/css/styles/all-themes.css",
                    "~/App_Assets/Admin/js/bundles/bootstrap-colorpicker/dist/css/bootstrap-colorpicker.css",
                    "~/App_Assets/Admin/js/bundles/multiselect/css/multi-select.css",
                    "~/App_Assets/Admin/css/form.min.css"
                    ));
            bundles.Add(new ScriptBundle("~/Admin/script").Include(
                        "~/App_Assets/Admin/js/app.min.js",
                        "~/App_Assets/Admin/js/admin.js",
                        "~/App_Assets/Admin/js/form.min.js",
                        "~/App_Assets/Admin/js/pages/forms/advanced-form-elements.js",
                        "~/App_Assets/Admin/js/bundles/multiselect/js/jquery.multi-select.js",
                        "~/App_Assets/Admin/js/bundles/bootstrap-colorpicker/dist/js/bootstrap-colorpicker.js",
                        "~/App_Assets/Admin/js/pages/forms/form-wizard.js"
                        ));
             //Custom Bundling Public Side
             bundles.Add(new StyleBundle("~/Public/css").Include(
                        "~/App_Assets/Public/css/bootstrap.min.css",
                        "~/App_Assets/Public/css/tobii.min.css",
                        "~/App_Assets/Public/css/materialdesignicons.min.css",
                        "~/App_Assets/Public/css/tiny-slider.css",
                        "~/App_Assets/Public/css/style.min.css",
                        "~/App_Assets/Public/css/colors/default.css",
                        "~/App_Assets/Public/css/unicons.iconscout.com/release/v3.0.6/css/line.css"
                        ));
            bundles.Add(new ScriptBundle("~/Public/script").Include(
                  "~/App_Assets/Public/js/bootstrap.bundle.min.js",
                  "~/App_Assets/Public/js/tiny-slider.js",
                  "~/App_Assets/Public/js/tobii.min.js",
                  "~/App_Assets/Public/js/feather.min.js",
                 // "~/App_Assets/Public/js/switcher.js",
                  "~/App_Assets/Public/js/plugins.init.js",
                  "~/App_Assets/Public/js/app.js"
                  ));
           
        }
    }
}
