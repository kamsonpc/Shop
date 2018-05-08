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
	public class ProductController : Controller
	{
		private readonly IProductMenagerService _productMenager;
		public ProductController(IProductMenagerService productMenager)
		{
			_productMenager = productMenager;
		}

		// GET: Product
		public ActionResult Index()
		{
			var products = Mapper.Map<List<Product>, List<ProductViewModel>>(_productMenager.GetAll());
			return View(products);
		}

		// GET: Product/Details/5
		public ActionResult Details(int id)
		{
			var product = Mapper.Map<Product, ProductViewModel>(_productMenager.GetById(id));
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
				if (file.ContentLength > 0 && file.ContentLength < 327680 && file.ContentType.Contains("image"))
				{
					string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
					file.SaveAs(path);

					productViewModel.Img = file.FileName;

					var product = Mapper.Map<ProductViewModel, Product>(productViewModel);
					_productMenager.AddNew(product);

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
			var product = Mapper.Map<Product, ProductViewModel>(_productMenager.GetById(id));
			return View(product);
		}

		// POST: Product/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, ProductViewModel productViewModel)
		{
			try
			{
				var product = Mapper.Map<ProductViewModel, Product>(productViewModel);
				_productMenager.Update(id, product);
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
			_productMenager.Remove(id);
			return RedirectToAction("Index");
		}


	}
}
