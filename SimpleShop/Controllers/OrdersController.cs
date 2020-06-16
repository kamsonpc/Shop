using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using SimpleShop.Extensions;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Controllers
{
	[Authorize]
	public partial class OrdersController : BaseController
	{
		private readonly IOrderService _orderService;


		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		public virtual ActionResult Index(int? page)
		{
			var pageNumber = page ?? 1;
			var orders = _orderService.GetByUserId(User.Identity.GetUserId())
				.MapTo<IEnumerable<OrdersPageVM>>()
				.ToPagedList(pageNumber, PageSize);

			return View(orders);
		}
	}
}