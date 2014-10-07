using System.Web;
using System.Web.Optimization;

namespace Dayspent.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/scripts/jquery-ui-{version}.js",
                        //"~/scripts/jquery-ui-1.11.1.js",
                        "~/scripts/jquery.datepicker.localization.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                       "~/Scripts/jquery.cookie.js",
                       "~/Scripts/jquery.blockui.js",
                       "~/Scripts/jquery.tagit.js",
                       "~/Scripts/moment.js",
                       "~/Scripts/moment-with-lang.js",
                       "~/scripts/markdowndeeplib.min.js",
                       "~/scripts/dropzone/dropzone.js",
                       "~/scripts/jquery.autosize.js",
                       "~/scripts/popModal.js",
                       "~/scripts/rangyinputs-jquery-1.1.2.js",
                       "~/scripts/chart.js",
                       "~/scripts/jquery.timepicker.js",
                       "~/scripts/jquery.inputmask/jquery.inputmask.js",
                       "~/scripts/jquery.inputmask/jquery.inputmask.extensions.js",
                       "~/scripts/jquery.inputmask/jquery.inputmask.date.extensions.js",
                       "~/scripts/jquery.inputmask/jquery.tagit.js"

                       ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/metro-ui").Include(
                        //"~/scripts/metro-ui/jquery.ui.widget.js",
                        //"~/scripts/metro-ui/metro.min.js",
                        "~/scripts/metro-ui/js/metro-global.js",
                        "~/scripts/metro-ui/js/metro-core.js",
                        "~/scripts/metro-ui/js/metro-locale.js",
                        "~/scripts/metro-ui/js/metro-accordion.js",
                        "~/scripts/metro-ui/js/metro-button-set.js",
                        "~/scripts/metro-ui/js/metro-calendar.js",
                        "~/scripts/metro-ui/js/metro-carousel.js",
                        "~/scripts/metro-ui/js/metro-countdown.js",
                        "~/scripts/metro-ui/js/metro-date-format.js",
                        //"~/scripts/metro-ui/js/metro-datepicker.js",
                        "~/scripts/metro-ui/js/metro-dialog-custom.js",
                        "~/scripts/metro-ui/js/metro-drag-tile.js",
                        //"~/scripts/metro-ui/js/metro-dropdown.js",
                        "~/scripts/metro-ui/js/metro-dropdown-custom.js",
                        "~/scripts/metro-ui/js/metro-fluentmenu.js",
                        "~/scripts/metro-ui/js/metro-hint.js",
                        "~/scripts/metro-ui/js/metro-initiator.js",
                        "~/scripts/metro-ui/js/metro-input-control.js",
                        "~/scripts/metro-ui/js/metro-listview.js",
                        "~/scripts/metro-ui/js/metro-live-tile.js",
                        //"~/scripts/metro-ui/js/metro-loader.js",
                        "~/scripts/metro-ui/js/metro-notify-custom.js",
                        "~/scripts/metro-ui/js/metro-panel.js",
                        "~/scripts/metro-ui/js/metro-plugin-template.js",
                        "~/scripts/metro-ui/js/metro-progressbar.js",
                        "~/scripts/metro-ui/js/metro-pull.js",
                        "~/scripts/metro-ui/js/metro-rating.js",
                        "~/scripts/metro-ui/js/metro-scroll.js",
                        "~/scripts/metro-ui/js/metro-slider.js",
                        "~/scripts/metro-ui/js/metro-stepper.js",
                        "~/scripts/metro-ui/js/metro-streamer.js",
                        "~/scripts/metro-ui/js/metro-tab-control.js",
                        "~/scripts/metro-ui/js/metro-table.js",
                        "~/scripts/metro-ui/js/metro-tile-transform.js",
                        "~/scripts/metro-ui/js/metro-times.js",
                        "~/scripts/metro-ui/js/metro-touch-handler.js",
                        "~/scripts/metro-ui/js/metro-treeview.js",
                        "~/scripts/metro-ui/js/metro-wizard.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                        "~/scripts/knockout-{version}.js",
                        "~/scripts/knockout.mapping-latest.js",
                        "~/scripts/knockout.bindings.js",
                        "~/scripts/knockout.bindings.editor.js",
                        "~/scripts/knockout.bindings.dayspent.js",
                        "~/scripts/knockout.functions.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/sammy").Include(
                        "~/scripts/sammy-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/scripts/commons.js",
                        "~/scripts/app/router.js",
                        "~/scripts/app/ui.js",
                        "~/scripts/app/app.js",
                        "~/scripts/app/main.js"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/widgets").Include(
                        "~/scripts/widgets/appnavigationbar.js",
                        "~/scripts/widgets/errordialog.js",
                        "~/scripts/widgets/timelinesidebar.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/views").Include(
                        "~/scripts/repositories/activityrepository.js",
                        "~/scripts/repositories/timelinerepository.js",
                        "~/scripts/repositories/userprofilerepository.js",
                        "~/scripts/repositories/useravatarrepository.js",
                        "~/scripts/repositories/activitytagrepository.js",
                        "~/scripts/repositories/tagrepository.js",
                        "~/scripts/viewmodels/activityviewmodel.js",
                        "~/scripts/viewmodels/timelineviewmodel.js",
                        "~/scripts/viewmodels/createactivityviewmodel.js",
                        "~/scripts/viewmodels/profileviewmodel.js",
                        "~/scripts/viewmodels/taggroupviewmodel.js",
                        "~/scripts/viewmodels/reportviewmodel.js",
                        "~/scripts/viewmodels/dashboardviewmodel.js",
                        "~/scripts/views/profile.js",
                        "~/scripts/views/timeline.js"
                         ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/metro-ui/css/metro-bootstrap.css",
                        "~/Content/metro-ui/css/metro-bootstrap-responsive.css",
                        "~/content/metro-ui/css/iconFont.min.css",
                        "~/content/metro-bootstrap-custom.css",
                        "~/content/jquery-ui.css",
                        "~/content/mdd_styles.css",
                        "~/content/popmodal.css",
                        "~/content/tagit.css",
                        "~/content/jquery.timepicker.css",
                        "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;
        }
    }
}
