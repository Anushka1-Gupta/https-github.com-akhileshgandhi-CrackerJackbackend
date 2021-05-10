using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace AngularJSAuthentication.API.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Setting the bundle to use CDN path.  
            bundles.UseCdn = true;

            //Referencing the CDN path for the styleBundle (bootStrap).  
            bundles.Add(new StyleBundle("~/bundle/css", "http://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css"));



            //referencing the our page specific javascript files.  
            bundles.Add(new ScriptBundle("~/bundles/controllerJs").Include("~/controller/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/controllerLogJs").Include("~/controller/Logs/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/controllerPermissionJs").Include("~/controller/permission/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/controllerCashMgmtJs").Include("~/controller/CashManagement/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/servicesJs").Include("~/services/*.js"));


            bundles.Add(new ScriptBundle("~/bundles/rptJS").Include("~/ReportsControllers/Comparison1Ctrl.js"));

            ////bundles.Add(new ScriptBundle("~/bundles/GroupSmsJs")
            ////                .Include("~/services/GroupSmsServices.js")
            ////               .Include("~/controller/GroupSMSController.js")
            ////    //.Include("~/controller/CustomerGroup.js")
            ////    );

            bundles.Add(new ScriptBundle("~/bundles/workJS")

               );

            bundles.Add(new ScriptBundle("~/bundles/AllNewJs")

      );

            bundles.Add(new ScriptBundle("~/bundles/Alljs")
                                         );

            //For enabling optimization forcefully.  


            string environment = ConfigurationManager.AppSettings["Environment"];
            if (environment != "development")
            {
                BundleTable.EnableOptimizations = true;
            }

        }
    }
}