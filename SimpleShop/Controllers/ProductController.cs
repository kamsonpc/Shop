using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using SimpleShop.Filters;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IOrderService _order;
		private readonly IProductService _product;
		private readonly ICategoryService _category;
		public ProductController(IProductService product, IOrderService order, ICategoryService category)
		{
			_product = product;
			_order = order;
			_category = category;
		}

		[AllowAnonymous]
		public ActionResult Index()
		{
			var products = _product.GetAll();
			var categories = _category.GetAll();

			CategoryProductVM categoryProducts = new CategoryProductVM
			{
				product = products,
				categories = categories
			};

			return View(categoryProducts);
		}

		[AllowAnonymous]
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var product = _product.GetById(id.Value);

			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			return View(product);
		}

		[AuthorizeCustom(Roles = "Administrator")]
		public ActionResult Create()
		{
			ProductVM productVm = new ProductVM
			{
				Categories = _category.GetSelectList()
			};

			return View(productVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[AuthorizeCustom(Roles = "Administrator")]
		public ActionResult Create(ProductVM productVm, HttpPostedFileBase file)
		{
			if (!ModelState.IsValid)
			{
				productVm.Categories = _category.GetSelectList();
				return View(productVm);
			}

			if (file.ContentLength > 0 && file.ContentLength < 327680 && file.ContentType.Contains("image"))
			{
				try
				{
					productVm.Img = _product.UploadImage(file);
					_product.AddNew(productVm);

					return RedirectToAction("Index");
				}
				catch
				{
					ViewBag.Message = "File upload failed!!";
					return View();
				}
			}

			ViewBag.Message = "File bad file type";
			productVm.Categories = _category.GetSelectList();
			return View(productVm);
		}

		[AuthorizeCustom(Roles = "Administrator")]
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var product = _product.GetById(id.Value);
			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			product.Categories = _category.GetSelectList();
			return View(product);
		}

		[AuthorizeCustom(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int? id, ProductVM productVm)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (!ModelState.IsValid)
			{
				productVm.Categories = _category.GetSelectList();

				return View(productVm);

			}

			_product.Update(id.Value, productVm);
			return RedirectToAction("Index");
		}

		[AuthorizeCustom(Roles = "Administrator")]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			_product.Remove(id.Value);
			return RedirectToAction("Index");
		}


		[AllowAnonymous]
		public ActionResult Category(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var products = _product.GetByCategory(id.Value);
			var categories = _category.GetAll();

			CategoryProductVM categoryProducts = new CategoryProductVM
			{
				product = products,
				categories = categories
			};

			return View("Index", categoryProducts);
		}

		[AllowAnonymous]
		[HttpGet]
		public ActionResult Search(string name)
		{
			var products = _product.GetAll();
			var categories = _category.GetAll();

			if (name != "")
			{
				products = products.FindAll(m => m.Name.Contains(name));
			}

			CategoryProductVM categoryProducts = new CategoryProductVM
			{
				product = products,
				categories = categories
			};
			return View("Index", categoryProducts);
		}

		[AllowAnonymous]
		[HttpGet]
		public ActionResult SearchByPrice(string min,string max)
		{
			int minimumPrice;
			int maximumPrice;
			try
			{
				maximumPrice = Int32.Parse(max);
				minimumPrice = Int32.Parse(min);
			}
			catch (FormatException e)
			{
				Console.WriteLine(e);
				maximumPrice = 0;
				minimumPrice = 0;
			}
			var products = _product.GetByPrice(minimumPrice,maximumPrice);
			var categories = _category.GetAll();
			
			CategoryProductVM categoryProducts = new CategoryProductVM
			{
				product = products,
				categories = categories
			};
			return View("Index", categoryProducts);
		}

	}
}
