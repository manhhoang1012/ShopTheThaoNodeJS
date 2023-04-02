using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FashionShop.Shared;
namespace FashionShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start()
        {
            Session[Const.AdminSession] = ""; // Supper Admin
            Session[Const.AdminIdSession] = ""; // Supper Admin

            Session[Const.CustomerSession] = ""; // Customer
            Session[Const.CustomerIdSession] = ""; // Customer

            Session[Const.CartSession] = Guid.NewGuid().ToString();
        }
    }
}
