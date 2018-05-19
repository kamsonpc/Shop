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
	[Authorize(Roles = "Administrator")]
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
			var products = Mapper.Map<List<Product>, List<ProductViewModel>>(_product.GetAll());
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
			var product = Mapper.Map<Product, ProductViewModel>(_product.GetById(id));
			return View(product);
		}

		// GET: Product/Create
		public ActionResult Create()
		{
			ProductViewModel productViewModel = new ProductViewModel
			{
				Categories = _category.GetSelectList()
			};

			return View(productViewModel);
		}

		// POST: Product/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ProductViewModel productViewModel, HttpPostedFileBase file)
		{
			try
			{
				if (file.ContentLength > 0 && file.ContentLength < 327680 && file.ContentType.Contains("image") && ModelState.IsValid)
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
			product.Categories = _category.GetSelectList();
			return View(product);
		}

		// POST: Product/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
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
			var product = _product.GetById(id);
			if (product != null)
			{
				_product.ChangeQuantity(id, 1);

				Order order = new Order
				{
					ApplicationUserId = User.Identity.GetUserId(),
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
			var products = Mapper.Map<List<Product>, List<ProductViewModel>>(_product.GetByCategory(id));
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
			var products = Mapper.Map<List<Product>, List<ProductViewModel>>(_product.GetAll());
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
