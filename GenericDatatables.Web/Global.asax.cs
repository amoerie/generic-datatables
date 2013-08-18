using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GenericDatatables.Datatables;
using GenericDatatables.Datatables.Remote;
using GenericDatatables.Datatables.Remote.Request;
using GenericDatatables.Default.Database;
using StackExchange.Profiling;

namespace GenericDatatables.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            DatatableConfig.Register();

            ModelBinders.Binders.Add(typeof(DatatableRequest), new DatatableRequestModelBinder());
            Database.SetInitializer(new GymInitializer());
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }

    }


}