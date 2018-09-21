using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using SimpleShop.Areas.Client.Models.Orders;
using SimpleShop.Data.Extensions;
using SimpleShop.Data.Interfaces.Services;
using SimpleShop.Helpers;

namespace SimpleShop.Areas.Client.Controllers
{
	[Authorize]
	public partial class OrdersController : BaseController
	{
		private readonly IOrderService _orderService;
		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		public virtual ActionResult Index()
		{
			var orders = _orderService.GetByUserId(User.Identity.GetUserId())
				.MapTo<IEnumerable<OrdersPageVM>>();

			return View(MVC.Client.Orders.Views.List, orders);
		}
	}
}