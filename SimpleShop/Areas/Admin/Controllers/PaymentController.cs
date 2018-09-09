using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using PagedList;
using SimpleShop.Areas.Client.Models.Orders;
using SimpleShop.Data.Extensions;
using SimpleShop.Data.Interfaces.Services;
using SimpleShop.Data.Models;
using SimpleShop.Filters;
using SimpleShop.Helpers;

namespace SimpleShop.Areas.Admin.Controllers
{
	[AuthorizeCustom(RoleTypes.Administrator)]
	public partial class PaymentController : BaseController
	{
		private readonly IOrderService _orderService;

		public PaymentController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public virtual ActionResult Index(string search, int? page)
		{
			var pageNumber = page ?? 1;
			var orders = string.IsNullOrEmpty(search) ? _orderService.GetAll() : _orderService.Find(search);

			var result = orders.MapTo<List<OrdersPageVM>>()
			   .ToPagedList(pageNumber, pageSize);

			ViewBag.Search = search;

			return View(result);
		}

		public virtual ActionResult Pay(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			_orderService.Pay(id.Value);
			return RedirectToAction(MVC.Admin.Payment.Index());

		}

		public virtual ActionResult Shipping(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var shippingData = _orderService.GetShippinDataById(id.Value).MapTo<ShippingVM>();
			if (shippingData == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			return View(shippingData);
		}
	}
}