using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
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

		// GET: Product
		[AllowAnonymous]
		public ActionResult Index()
		{
			var products = Mapper.Map<List<Product>, List<ProductVM>>(_product.GetAll());
			var categories = _category.GetAll();

			CategoryProductVM categoryProducts = new CategoryProductVM
			{
				product = products,
				categories = categories
			};

			return View(categoryProducts);
		}

		// GET: Product/Details/5
		[AllowAnonymous]
		public ActionResult Details(int id)
		{
			var product = Mapper.Map<Product, ProductVM>(_product.GetById(id));
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		[Authorize(Roles = "Administrator")]
		public ActionResult Create()
		{
			ProductVM productVm = new ProductVM
			{
				Categories = _category.GetSelectList()
			};

			return View(productVm);
		}

		// POST: Product/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Administrator")]
		public ActionResult Create(ProductVM productVm, HttpPostedFileBase file)
		{
			if (file.ContentLength > 0 && file.ContentLength < 327680 && file.ContentType.Contains("image") && ModelState.IsValid)
			{
				try
				{
					productVm.Img = _product.UploadImage(file);

					var product = Mapper.Map<ProductVM, Product>(productVm);
					_product.AddNew(product);

					return RedirectToAction("Index");

				}
				catch
				{
					ViewBag.Message = "File upload failed!!";
					return View();
				}
			}

			ViewBag.Message = "File bad file type";
			return View();
		}

		[Authorize(Roles = "Administrator")]
		public ActionResult Edit(int id)
		{
			var product = Mapper.Map<Product, ProductVM>(_product.GetById(id));
			if (product == null)
			{
				return HttpNotFound();
			}
			product.Categories = _category.GetSelectList();
			return View(product);
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, ProductVM productVm)
		{
			if (!ModelState.IsValid)
			{
				return View(productVm);
			}
			var product = Mapper.Map<ProductVM, Product>(productVm);
			if (_product.Update(id, product))
			{
				return RedirectToAction("Index");

			}
			else
			{
				return View(productVm);
			}

		}

		[Authorize(Roles = "Administrator")]
		public ActionResult Delete(int id)
		{
			_product.Remove(id);
			return RedirectToAction("Index");
		}

		[Authorize]
		public ActionResult Buy(int id)
		{
			var product = _product.GetById(id);
			var user = User.Identity.GetUserId();
			if (product != null && user != null)
			{
				_product.ChangeQuantity(id, 1);

				Order order = new Order
				{
					ApplicationUserId = user,
					ProductId = product.ProductId,
					Date = DateTime.Now,
					Price = product.Price,
					Quantity = 1,
					Payment = false
				};
				_order.AddNew(order);
			}
			return RedirectToAction("Index", "Orders");
		}

		[AllowAnonymous]
		public ActionResult Category(int id)
		{
			var products = Mapper.Map<List<Product>, List<ProductVM>>(_product.GetByCategory(id));
			var categories = _category.GetAll();

			CategoryProductVM categoryProducts = new CategoryProductVM
			{
				product = products,
				categories = categories
			};

			return View("Index", categoryProducts);
		}

		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Search(string name)
		{
			var products = Mapper.Map<List<Product>, List<ProductVM>>(_product.GetAll());
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

	}
}
