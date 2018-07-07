using System.Web.Mvc;
using static SimpleShop.Views.Shared.Alerts.Alert;

namespace SimpleShop.Controllers
{
	public abstract partial class BaseController : Controller
	{
		public void Alert(string message, NotificationType notificationType)
		{
			TempData["notification"] = message;
		}
	}
}