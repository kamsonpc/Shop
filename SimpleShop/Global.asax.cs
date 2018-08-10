using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using SimpleShop.Areas.Admin;
using SimpleShop.Areas.Admin.Models;
using SimpleShop.Areas.Client;
using SimpleShop.Areas.Client.Models;

namespace SimpleShop
{
	public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
	        AreaRegistration.RegisterAllAreas();
	        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
	        IocUnityConfig.RegisterServices();
	        GlobalConfiguration.Configure(WebApiConfig.Register);
	        Mapper.Initialize(cfg =>
	        {
		        cfg.AddProfile<AdminMapping>();
		        cfg.AddProfile<ClientMapping>();
	        });
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new RazorViewEngine());
		}
	}
}
