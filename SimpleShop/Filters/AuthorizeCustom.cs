using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleShop.Filters
{

    public class AuthorizeCustom : AuthorizeAttribute
    {
        public AuthorizeCustom(params object[] roles)
        {
            if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("roles");

            this.Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "AuthorizationError" }));
            }
        }
    }
}