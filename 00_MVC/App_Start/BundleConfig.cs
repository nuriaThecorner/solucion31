using System.Web;
using System.Web.Optimization;

namespace _00_MVC
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/style.min.css",
                      "~/Scripts/owlcarousel/assets/owl.carousel.css",
                      "~/Scripts/owlcarousel/assets/owl.carousel.min.css",
                      "~/Scripts/owlcarousel/assets/owl.theme.default.css",
                      "~/Scripts/owlcarousel/assets/owl.theme.default.min.css",
                      "~/Scripts/owlcarousel/assets/owl.theme.green.css",
                      "~/Scripts/owlcarousel/assets/owl.theme.green.min.css",
                      "~/Scripts/owlcarousel/assets/owl.video.play.css",
                      "~/Content/styles.css"));


            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                "~/Scripts/main.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/waypoints.min").Include(
                "~/Scripts/waypoints/waypoints.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/owlcarousel").Include(
                "~/Scripts/owlcarousel/owl.carousel.js"));
            bundles.Add(new ScriptBundle("~/bundles/carousel.min").Include(
                "~/Scripts/owlcarousel/owl.carousel.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/easing.min").Include(
                "~/Scripts/easing/easing.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/easing").Include(
                "~/Scripts/easing/easing.js"));

            bundles.Add(new ScriptBundle("~/bundles/counterup").Include(
                "~/Scripts/counterup/counterup.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/contact").Include(
                "~/Scripts/mail/contact.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqBootstrapValidation.min").Include(
                "~/Scripts/mail/jqBootstrapValidation.min"));


            bundles.Add(new ScriptBundle("~/bundles/jquery-3.4.1").Include(
                "~/Scripts/jquery-3.4.1.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap.bundle.min").Include(
                "~/Scripts/bootstrap.bundle.min.js"));
        }
    }
}
