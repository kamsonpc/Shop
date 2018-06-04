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
				if(!ModelState.IsValid)
				{
					return RedirectToAction("Index");
				}

				var productToBuyList = (List<ProductVM>)Session["Cart"];
				string UserId = User.Identity.GetUserId();

				foreach (var product in productToBuyList) 
				{
					if(_product.Quantity(product.ProductId) - product.CustomerQuantity > 0)
					{
						_order.AddNew(product, shippingData, UserId);
					}
				}
				Session["Cart"] = null;
			}
			return RedirectToAction("Index", "Orders");
		}

		[HttpGet]
		public ActionResult Add(int ProductId,int quantity)
		{
			var product = _product.GetById(ProductId);
			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			if (Session["Cart"] == null)
			{
				Session["Cart"] = new List<ProductVM>();
			}

			product.CustomerQuantity = quantity;

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