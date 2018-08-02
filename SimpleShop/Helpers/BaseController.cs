using System.Web.Mvc;

namespace SimpleShop.Helpers
{
	public abstract partial class BaseController : Controller
	{
		public const int pageSize = 10;

		public void Alert(string message, NotificationType notificationType)
		{
			TempData["notification"] = message;
		}
	}
}