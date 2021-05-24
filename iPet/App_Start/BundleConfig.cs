using System.Web;
using System.Web.Optimization;

namespace iPet
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender com ela. Após isso, quando você estiver
            // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.

            bundles.Add(new ScriptBundle("~/PixelAdmin/js").Include(
                "~/js/require.js",
                "~/js/requirejs-config.js"
                ));

            bundles.Add(new StyleBundle("~/css/fileInput").Include(
                    "~/Content/bootstrap-fileinput/css/fileinput.min.css"
                ));

            bundles.Add(new StyleBundle("~/css/datepicker").Include(
                    "~/Content/bootstrap-datepicker.min.css"
                ));

            bundles.Add(new StyleBundle("~/css/datatables").Include(
                    "~/Content/DataTables/css/dataTables.bootstrap.min.css",
                    "~/Content/DataTables/css/dataTables.jquerui.min.css"
                ));

            bundles.Add(new StyleBundle("~/PixelAdmin/css").Include(
                      "~/Content/Layout/bootstrap.min.css",
                      "~/Content/fontawesome.min.css",
                      "~/Content/fontawesome-all.min.css",
                      "~/Content/Layout/pixeladmin.min.css",
                      "~/Content/Layout/widgets.min.css",
                      "~/Content/Layout/themes/candy-green.min.css",
                      "~/js/demo/demo.css",
                      "~/Content/Site.css",
                      "~/Content/animate.min.css"
                      ));

            bundles.Add(new StyleBundle("~/Login/css").Include(
                    "~/Content/Layout/login-custom.css"
                ));

            bundles.Add(new StyleBundle("~/Loading/css").Include(
                    "~/Content/loading.css"
                ));

            bundles.Add(new StyleBundle("~/Errors/css").Include(
                    "~/Content/ErrorPage/Error404.css",
                    "~/Content/ErrorPage/Error500.css"
                ));

            bundles.Add(new StyleBundle("~/Public/css").Include(
                    "~/Content/Layout/public-layout/vendor/bootstrap/css/bootstrap.min.css",
                    "~/Content/Layout/public-layout/vendor/font-awesome/css/font-awesome.min.css",
                    "~/Content/Layout/public-layout/vendor/magnific-popup/css/magnific-popup.css",
                    "~/Content/Layout/public-layout/freelancer.min.css",
                    "~/Content/Layout/pixeladmin.min.css",
                    "~/Content/Layout/widgets.min.css",
                    "~/Content/Layout/themes/candy-green.min.css",
                    "~/Content/Site.css"
                ));

            bundles.Add(new ScriptBundle("~/Public/js").Include(
                "~/js/require.js",
                "~/js/requirejs-config.js"
                ));
        }
    }
}