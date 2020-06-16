using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SimpleShop.Extensions;
using SimpleShop.Filters;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;
using SimpleShop.T4MVC;
using static SimpleShop.Views.Shared.Alerts.Alert;


namespace SimpleShop.Controllers
{
	[AuthorizePolicy]
	public partial class CartController : BaseController
	{
		private readonly ICartService _cartService;

		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}
		public virtual ActionResult Index()
		{
			var userId = User.Identity.GetUserId();
			var items = _cartService.GetAll(userId).MapTo<List<CartVM>>();
			return View(items);
		}

		[HttpGet]
		public virtual ActionResult Add(int productId, int orderQuantity)
		{
			var userId = User.Identity.GetUserId();

			var cartItem = new Cart
			{
				ProductId = productId,
				ApplicationUserId = userId,
				OrderedQuantity = orderQuantity
			};

			_cartService.Add(cartItem);

			return RedirectToAction(MVC.Cart.Index());
		}

		[HttpGet]
		public virtual ActionResult Remove(int id)
		{
			_cartService.Remove(id);

			return RedirectToAction(MVC.Cart.Index());
		}

		public virtual ActionResult Complete()
		{
			var userId = User.Identity.GetUserId();
			var cartItemCount = _cartService.Counter(userId);

			if (cartItemCount == 0) return RedirectToAction(MVC.Product.Index());
			return View();
		}

		[HttpPost]
		public virtual ActionResult Complete(ShippingVM shippingData)
		{

			var userId = User.Identity.GetUserId();

			if (!ModelState.IsValid) return View(shippingData);

			var boughtItemsNumber = _cartService.Complete(userId, shippingData);

			Alert("You bought " + boughtItemsNumber + " Items", NotificationType.success);
			return RedirectToAction(MVC.Orders.Index());
		}

		public virtual ActionResult Counter()
		{
			var userId = User.Identity.GetUserId();
			var cartItemsCount = _cartService.Counter(userId);
			return Json(cartItemsCount, JsonRequestBehavior.AllowGet);
		}
	}
}