namespace SalesReport
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery-1.9.1.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                "~/Scripts/bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/moment").Include(
                "~/Scripts/moment.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap-datetimepicker").Include(
                "~/Scripts/bootstrap-datetimepicker.js"
                ));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/Content/bootstrap-datetimepicker").Include(
                "~/Content/bootstrap-datetimepicker.css"
                ));
        }
    }
}