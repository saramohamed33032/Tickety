using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace FinalProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/fontawesome-all.min.css",
                      "~/Content/site.css"));
            //ScriptBundle scriptBndl = new ScriptBundle("~/bundles/bootstrap");
            //scriptBndl.Include(
            //                         "~/Scripts/bootstrap.js",
            //                         "~/Scripts/respond.js",
            //                         "~/Scripts/jquery-{version}.js",
            //                         "~/Scripts/jquery.validate*",
            //                         "~/Scripts/modernizr-*",
            //                         "~/Content/fontawesome-all.min.css",
            //          "~/Content/site.css");
            //bundles.Add(scriptBndl);

            //BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/table").Include("~/Scripts/dataTable.js"));
            bundles.Add(new ScriptBundle("~/bundles/table").Include("~/Scripts/dataTables.bootstrap4.js"));
        }
    }

   
}
