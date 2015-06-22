using System;
using System.Web;
using System.Web.Optimization;

namespace ADOPets.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);


            bundles.Add(new ScriptBundle("~/bundles/headerJS").Include(
                       "~/Scripts/external/jquery-1.11.1.min.js",
                       "~/Scripts/external/bootstrap.min.js",
                       "~/Scripts/external/viewportchecker.js",
                       "~/Scripts/external/moment.js",
                       "~/Scripts/external/bootstrap-datetimepicker.min.js",
                       "~/Scripts/external/enscroll-0.6.1.min.js",
                       "~/Scripts/external/jquery.validate.min.js",
                       "~/Scripts/external/expressive.annotations.analysis.js",
                       "~/Scripts/external/expressive.annotations.validate.js",
                       "~/Scripts/external/jquery.validate.unobtrusive.min.js",
                       "~/Scripts/external/jquery.unobtrusive-ajax.js",
                       "~/Scripts/external/jquery.cookie.min.js"));


            bundles.Add(new StyleBundle("~/Content/css/signupCSS").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/font.css",
                      "~/Content/css/bootstrap-datetimepicker.min.css",
                      "~/Content/css/signup2.css",
                      "~/Content/css/datepicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/signupJS").Include(
                     "~/Scripts/external/jquery-1.11.1.min.js",
                     "~/Scripts/external/bootstrap.min.js",
                     "~/Scripts/external/viewportchecker.js",
                     "~/Scripts/external/moment.js",
                     "~/Scripts/external/bootstrap-datetimepicker.min.js",
                     "~/Scripts/external/enscroll-0.6.1.min.js",
                     "~/Scripts/external/jquery.validate.min.js",
                     "~/Scripts/external/expressive.annotations.analysis.js",
                     "~/Scripts/external/expressive.annotations.validate.js",
                     "~/Scripts/external/jquery.validate.unobtrusive.min.js",
                     "~/Scripts/external/jquery.unobtrusive-ajax.js",
                     "~/Scripts/external/jquery.cookie.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/signupFooterJS").Include(
                    "~/Scripts/external/globalize.js",
                    "~/Scripts/external/globalize.culture.in.js",
                    "~/Scripts/external/globalize.culture.fr.js",
                    "~/Scripts/external/globalize.culture.pt-BR.js",
                    "~/Scripts/external/bootstrap-datepicker.js",
                    "~/Scripts/external/prefixfree.min.js",
                  "~/Scripts/external/bootstrap-tabdrop.js"));


            bundles.Add(new StyleBundle("~/Content/css/styles").Include(
                        "~/Content/css/bootstrap.css",
                        "~/Content/css/custom.css",
                        "~/Content/css/custom_k.css",
                        "~/Content/css/animate.css",
                        "~/Content/css/font-awesome.css",
                        "~/Content/css/font.css",
                        "~/Content/css/fullcalendar.css",
                        "~/Content/css/fullcalendar.print.css",
                        "~/Content/css/bootstrap-datetimepicker.min.css",
                        "~/Content/css/datepicker.css",
                        "~/Content/css/jquery.dataTables.css",
                        "~/Content/css/dataTables.bootstrap.css",
                        "~/Content/css/tabdrop.css",
                        "~/Content/css/bootstrap-tour.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/midJS").Include(
                "~/Scripts/external/bootstrap-tour.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/footerJS").Include(
                         "~/Scripts/external/globalize.js",
                         "~/Scripts/external/globalize.culture.in.js",
                         "~/Scripts/external/globalize.culture.fr.js",
                         "~/Scripts/external/globalize.culture.pt-BR.js",
                         "~/Scripts/external/bootstrap-datepicker.js",
                         "~/Scripts/external/prefixfree.min.js",
                         "~/Scripts/external/highcharts.js",
                       "~/Scripts/external/bootstrap-tabdrop.js",
                       "~/Scripts/external/jquery.dataTables.js",
                       "~/Scripts/external/dataTables.bootstrap.js",
                       "~/Scripts/external/ui-custom.js"));
        }
        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
                throw new ArgumentNullException("ignoreList");
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            // ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }

    }
}