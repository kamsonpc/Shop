using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleShop.Areas.Client
{
	public class ClientAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Client";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Client_default",
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				new string[] { "SimpleShop.Areas.Client.Controllers" }
			);
		}
	}
}