using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using SimpleShop.Controllers;
using SimpleShop.Mapping;

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
			Mapper.Initialize(cfg => cfg.AddProfile<MappingProfiles>());
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new RazorViewEngine());
		}
	}
}
