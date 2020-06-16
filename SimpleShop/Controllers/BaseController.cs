using System.Web.Mvc;
using static SimpleShop.Views.Shared.Alerts.Alert;

namespace SimpleShop.Controllers
{
	public abstract  class BaseController : Controller
	{
		public const int PageSize = 10;

		public void Alert(string message, NotificationType notificationType)
		{
			TempData["notification"] = message;
		}
	}
}