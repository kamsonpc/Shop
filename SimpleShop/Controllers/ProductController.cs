using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;
using SimpleShop.Services;

namespace SimpleShop.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class ProductController : Controller
	{
		private readonly IOrderService _order;
		private readonly IProductService _product;
		public ProductController(IProductService product,IOrderService order)
		{
			_product = product;
			_order = order;
		}

		// GET: Product
		[AllowAnonymous]
		public ActionResult Index()
		{
			var products = Mapper.Map<List<Product>, List<ProductViewModel>>(_product.GetAll());
			return View(products);
		}

		// GET: Product/Details/5
		[AllowAnonymous]
		public ActionResult Details(int id)
		{
			var product = Mapper.Map<Product, ProductViewModel>(_product.GetById(id));
			return View(product);
		}

		// GET: Product/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Product/Create
		[HttpPost]
		public ActionResult Create(ProductViewModel productViewModel,HttpPostedFileBase file)
		{
			try
			{
				if (file.ContentLength > 0 && file.ContentLength < 327680 && file.ContentType.Contains("image") && ModelState.IsValid != false)
				{


					productViewModel.Img = _product.UploadImage(file);

					var product = Mapper.Map<ProductViewModel, Product>(productViewModel);
					_product.AddNew(product);

					return RedirectToAction("Index");
				}

				ViewBag.Message = "File bad file type";
				return View();

			}
			catch
			{
				ViewBag.Message = "File upload failed!!";
				return View();
			}
		}

		// GET: Product/Edit/5
		public ActionResult Edit(int id)
		{
			var product = Mapper.Map<Product, ProductViewModel>(_product.GetById(id));
			return View(product);
		}

		// POST: Product/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, ProductViewModel productViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(productViewModel);
			}
			try
			{
				var product = Mapper.Map<ProductViewModel, Product>(productViewModel);
				_product.Update(id, product);
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Product/Delete/5
		public ActionResult Delete(int id)
		{
			_product.Remove(id);
			return RedirectToAction("Index");
		}

		[AllowAnonymous]
		public ActionResult Buy(int id)
		{
			_product.ChangeQuantity(id,1);

			Order order = new Order();
			order.ApplicationUserId = User.Identity.GetUserId();
			order.ProductId = id;
			order.Date = DateTime.Now;

			_order.AddNew(order);
			return RedirectToAction("Index");
		}
	}
}
