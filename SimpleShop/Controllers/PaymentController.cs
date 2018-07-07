using System.Net;
using System.Web.Mvc;
using PagedList;
using SimpleShop.Filters;
using SimpleShop.Interfaces.Services;

namespace SimpleShop.Controllers
{
	[AuthorizeCustom(Roles = "Administrator")]
	public partial class PaymentController : BaseController
	{
		private readonly IOrderService _orderService;

		private const int numberProductOnPage = 10;

		public PaymentController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public virtual ActionResult Index(string search, int? page)
		{
			var pageNumber = page ?? 1;
			var orders = string.IsNullOrEmpty(search) ? _orderService.GetAll() : _orderService.Find(search);

			ViewBag.Search = search;

			return View(orders.ToPagedList(pageNumber, numberProductOnPage));
		}

		public virtual ActionResult Pay(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			_orderService.Pay(id.Value);
			return RedirectToAction(MVC.Payment.Index());

		}

		public virtual ActionResult Shipping(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var shippingData = _orderService.GetShippinDataById(id.Value);
			if (shippingData == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			return View(shippingData);
		}
	}
}