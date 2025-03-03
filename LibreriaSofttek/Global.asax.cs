using LibreriaSofttek.Interfaces;
using LibreriaSofttek.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity.Mvc5;
using Unity;

namespace LibreriaSofttek
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Configuración para inyección de dependencias
            var container = new UnityContainer();

            // Registro para implementación de interfaces
            container.RegisterType<ILibroService, LibroService>();
            container.RegisterType<IAutorService, AutorService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
