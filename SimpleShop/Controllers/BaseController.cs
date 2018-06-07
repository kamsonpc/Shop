using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SimpleShop.Views.Shared.Alerts.Alert;

namespace SimpleShop.Controllers
{
	public abstract class BaseController : Controller
	{
		public void Alert(string message, NotificationType notificationType)
		{
			var msg = "<div class='alert alert-" + notificationType + "' role='alert'>" +
				message + "</div>";
			TempData["notification"] = msg;

		}
	}
}