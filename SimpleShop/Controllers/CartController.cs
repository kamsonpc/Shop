using Microsoft.AspNet.Identity;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SimpleShop.Controllers
{
	[Authorize]
    public class CartController : Controller
    {
		private readonly IProductService _product;
		private readonly IOrderService _order;
		public CartController(IProductService product,IOrderService order)
		{		
			_product = product;
			_order = order;
		}
        // GET: Cart
        public ActionResult Index()
        {
			if (Session["Cart"] != null)
			{
				var cart = (List<ProductVM>)Session["Cart"];
				return View(cart);
			}
			return	RedirectToAction("Index", "Product");
        }

		public ActionResult Confirm()
		{
			return View("Buy");
		}


		[HttpPost]
		public ActionResult Confirm(ShippingVM shippingData)
		{
			if (Session["Cart"] != null)
			{
				var productToBuyList = (List<ProductVM>)Session["Cart"];
				foreach (var product in productToBuyList) 
				{
					Buy(product.ProductId, shippingData);
				}
				Session["Cart"] = null;
			}
			return RedirectToAction("Index", "Orders");
		}

		[Authorize]
		public ActionResult Buy(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var shipping = _product.GetById(id.Value);
			if (shipping == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			return View();

		}

		[Authorize]
		[HttpPost]
		public ActionResult Buy(int? id, ShippingVM shippingData)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var product = _product.GetById(id.Value);
			var user = User.Identity.GetUserId();

			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			if (!ModelState.IsValid)
			{

				return RedirectToAction("Index");
			}

			_product.ChangeQuantity(id.Value, 1);

			Order order = new Order
			{
				ApplicationUserId = user,
				ProductId = product.ProductId,
				Date = DateTime.Now,
				Price = product.Price,
				Quantity = 1,
				Payment = false,
				NameAndSurname = shippingData.NameAndSurname,
				PhoneNumber = shippingData.PhoneNumber,
				Address = shippingData.Address,
				CityCode = shippingData.CityCode,
				Country = shippingData.Country
			};

			_order.AddNew(order);
			return View("BuySuccess");
		}

		public ActionResult Add(int id)
		{
			var product = _product.GetById(id);
			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			if (Session["Cart"] == null)
			{
				Session["Cart"] = new List<ProductVM>();
			}

			var cart = (List<ProductVM>)Session["Cart"];
			cart.Add(product);
			Session["Cart"] = cart;

			return RedirectToAction("Index","Cart");
		}

		public ActionResult Remove(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (Session["Cart"] != null)
			{
				var cart = (List<ProductVM>)Session["Cart"];
				var productToRemove = cart.Find(p => p.ProductId == id);

				cart.Remove(productToRemove);
			}
			return RedirectToAction("Index");
		}
	}
}