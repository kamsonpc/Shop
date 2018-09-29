using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleShop
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.RouteExistingFiles = false;

			routes.LowercaseUrls = true;
			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new { area = "Client", controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
				null,
				new[] { "SimpleShop.Areas.Client.Controllers" }
			).DataTokens.Add("area", "Client");
		}
	}
}
