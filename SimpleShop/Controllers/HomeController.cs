using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProductService _product;

		public HomeController(IProductService product)
		{
			_product = product;
		}

		public ActionResult Index()
		{
			var products = Mapper.Map<List<Product>,List<ProductViewModel>>(_product.GetAll());
			var last_products = products.Skip(Math.Max(0, products.Count() - 3));
			return View(last_products);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}