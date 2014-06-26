using System.Collections.Generic;
using System.Web.Optimization;

namespace RMS.Centralize.Website
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {

            var str = new ScriptBundle("~/bundles/defaultJs").Include(
                "~/js/libs/jquery-2.0.2.min.js",
                "~/js/libs/jquery-ui-1.10.3.min.js",
                "~/js/bootstrap/bootstrap.min.js",
                "~/js/notification/SmartNotification.min.js",
                "~/js/smartwidgets/jarvis.widget.min.js",
                "~/js/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js",
                "~/js/plugin/sparkline/jquery.sparkline.min.js",
                "~/js/plugin/jquery-validate/jquery.validate.min.js",
                "~/js/plugin/masked-input/jquery.maskedinput.min.js",
                "~/js/plugin/select2/select2.min.js",
                "~/js/plugin/bootstrap-slider/bootstrap-slider.min.js",
                "~/js/plugin/msie-fix/jquery.mb.browser.min.js",
                "~/js/plugin/fastclick/fastclick.js",
                "~/js/plugin/jquery-form/jquery-form.min.js",
                "~/js/my.js",
                "~/js/app.js");
            str.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(str);

             str = new ScriptBundle("~/bundles/datatableJs").Include(
                "~/js/plugin/datatables/jquery.dataTables-cust.min.js", 
                "~/js/plugin/datatables/ColReorder.min.js", 
                "~/js/plugin/datatables/FixedColumns.min.js", 
                "~/js/plugin/datatables/ColVis.min.js", 
                "~/js/plugin/datatables/ZeroClipboard.js", 
                "~/js/plugin/datatables/media/js/TableTools.min.js", 
                "~/js/plugin/datatables/DT_bootstrap.js");
            str.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(str);

            str = new ScriptBundle("~/bundles/myJs").Include(
                "~/js/purl.js",
                "~/js/date.format.js",
                "~/js/plugin/noUiSlider/jquery.nouislider.min.js"
                );
            str.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(str);


}
    }
    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}