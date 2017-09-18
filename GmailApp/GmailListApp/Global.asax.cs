using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GmailListApp.IoC;
using System.Web.Optimization;
using GmailListApp.App_Start;

namespace GmailListApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
            Bootstrapper.ConfigureStructureMap(ConfigurationHelper.ConfigureDependencies);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
