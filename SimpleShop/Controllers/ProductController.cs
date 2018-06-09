using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SimpleShop.Filters;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.SearchModels;
using SimpleShop.Models.ViewsModels;
using static SimpleShop.Views.Shared.Alerts.Alert;

namespace SimpleShop.Controllers
{
	[Authorize]
	public class ProductController : BaseController
	{
		private readonly IOrderRepository _order;
		private readonly IProductRepository _product;
		private readonly ICategoryRepository _category;

		private const int numberProductOnPage = 9;

		public ProductController(IProductRepository product, IOrderRepository order, ICategoryRepository category)
		{
			_product = product;
			_order = order;
			_category = category;
		}
		
		public List<ProductVM> Filter(int? categoryId,ProductSearchModel search,List<ProductVM> products)
		{
			if (categoryId != null)
			{
				products = products.FindAll(p => p.CategoryId == categoryId);
			}

			if (!String.IsNullOrEmpty(search.Name))
			{
				products = products.FindAll(m => m.Name.Contains(search.Name));
			}
			if (search.PriceFrom != null && search.PriceTo != null)
			{
				products = products.FindAll(m => m.Price >= search.PriceFrom).FindAll(p => p.Price <= search.PriceTo);
			}

			return products;
		}

		[AllowAnonymous]
		public ActionResult Index(int? categoryId,ProductSearchModel search, int? page)
		{
			var pageNumber = page ?? 1;

			ViewBag.Search = search;
			ViewBag.CategoryId = categoryId;
			var categories = _category.GetAll();
			var products = _product.GetAll();

			
			ProductPageVM productPageVM = new ProductPageVM
			{
				Product = Filter(categoryId,search,products).ToPagedList(pageNumber,numberProductOnPage),
				Categories = categories,
				Search = search
			};

			return View(productPageVM);
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
					Alert("Dodano produkt" + productVm.Name, NotificationType.success);

					return RedirectToAction("Index");
				}
				catch
				{
					Alert("Wysyłanie pliku nie powiodło się", NotificationType.danger);
					return View();
				}
			}

			Alert("Plik jest nie prawidłowy", NotificationType.danger);
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
	}
}
