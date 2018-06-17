using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using SimpleShop.Interfaces.Services;

namespace SimpleShop.Controllers
{
	[Authorize]
	public class OrdersController : BaseController
	{
		private readonly IOrderService _orderService;

		private const int numberProductOnPage = 10;

		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		public ActionResult Index(int? page)
		{
			var pageNumber = page ?? 1;
			var orders = _orderService.GetByUserId(User.Identity.GetUserId()).ToPagedList(pageNumber, numberProductOnPage);

			return View(orders);
		}
	}
}