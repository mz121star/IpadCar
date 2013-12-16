using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace car
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Goods", // Route name
                "Home/LoadGoods/{typeId}/{viewType}", // URL with parameters
                new { controller = "Home", action = "LoadGoods", viewType = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Car", // Route name
                "Home/Car/{viewType}", // URL with parameters
                new { controller = "Home", action = "Car", viewType = UrlParameter.Optional } // Parameter defaults
            );
            
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{viewType}", // URL with parameters
                new { controller = "Home", action = "Index", viewType = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
