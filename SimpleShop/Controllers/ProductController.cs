using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;
using SimpleShop.Services;

namespace SimpleShop.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		// GET: Product
		[AllowAnonymous]
		public ActionResult Index()
		{
			var products = Mapper.Map<List<Product>, List<ProductViewModel>>(_productService.GetAll());
			return View(products);
		}

		// GET: Product/Details/5
		[AllowAnonymous]
		public ActionResult Details(int id)
		{
			var product = Mapper.Map<Product, ProductViewModel>(_productService.GetById(id));
			product.Quantity = _productService.CountProductUnits(id);
		
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


					productViewModel.Img = _productService.UploadImage(file);

					var product = Mapper.Map<ProductViewModel, Product>(productViewModel);
					int quantity = productViewModel.Quantity;

					_productService.AddNew(product,quantity);

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
			var product = Mapper.Map<Product, ProductViewModel>(_productService.GetById(id));
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
				_productService.Update(id, product);
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
			_productService.Remove(id);
			return RedirectToAction("Index");
		}


	}
}
