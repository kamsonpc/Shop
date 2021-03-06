﻿using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleShop.Filters
{

	public class AuthorizePolicy : AuthorizeAttribute
	{
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				base.HandleUnauthorizedRequest(filterContext);
			}
			else
			{
				filterContext.Result =
					new RedirectToRouteResult(new RouteValueDictionary(new {controller = "Account", action = "AuthorizationError" }));
			}
		}
	}
}