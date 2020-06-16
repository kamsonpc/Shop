using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SimpleShop.Extensions;
using SimpleShop.Filters;
using SimpleShop.Interfaces;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models;
using SimpleShop.Models.SearchModels;
using SimpleShop.Models.ViewsModels;
using SimpleShop.T4MVC;
using static SimpleShop.Views.Shared.Alerts.Alert;

namespace SimpleShop.Controllers
{
	[Authorize]
	public partial class ProductController : BaseController
	{
		private readonly IProductService _productService;
		private readonly IUnitOfWork _unitOfWork;

		public ProductController(IProductService productService, IUnitOfWork unitOfWork)
		{
			_productService = productService;
			_unitOfWork = unitOfWork;
		}

		[AllowAnonymous]
		public virtual ActionResult Index(int? categoryId, ProductSearchModel search, int? page)
		{
			var pageNumber = page ?? 1;

			ViewBag.Search = search;
			ViewBag.CategoryId = categoryId;

			var categories = _unitOfWork.Categories.GetAll().ToList();

			var productPageVm = new ProductPageVM
			{
				Product = _productService.Search(search, categoryId).MapTo<List<ProductVM>>()
					.ToPagedList(pageNumber, PageSize),
				Categories = categories,
				Search = search
			};

			return View(productPageVm);
		}


		[AllowAnonymous]
		public virtual ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var product = _productService.GetById(id.Value).MapTo<ProductVM>();

			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			return View(product);
		}

		[AuthorizePolicy(Roles = "Administrator")]
		public virtual ActionResult Create()
		{
			var productVm = new ProductVM
			{
				Categories = _unitOfWork.Categories.GetSelectList()
			};

			return View(productVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[AuthorizePolicy(Roles = "Administrator")]
		public virtual ActionResult Create(ProductVM productVm, HttpPostedFileBase file)
		{
			productVm.Categories = _unitOfWork.Categories.GetSelectList();

			if (!ModelState.IsValid)
			{
				return View(productVm);
			}

			if (file.ContentLength > 0 && file.ContentLength < 327680 && file.ContentType.Contains("image"))
			{
				try
				{
					productVm.Img = _productService.UploadImage(file);
					var product = productVm.MapTo<Product>();
					_productService.AddNew(product);
					Alert("Dodano produkt : " + product.Name, NotificationType.success);
					return RedirectToAction(MVC.Product.Index());
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					Alert("Nie udało się dodać", NotificationType.danger);
					return View(productVm);
				}
			}

			Alert("Invalid Image", NotificationType.danger);
			return View(productVm);

		}

		[AuthorizePolicy(Roles = "Administrator")]
		public virtual ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var productVm = _productService.GetById(id.Value).MapTo<ProductVM>();
			if (productVm == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			productVm.Categories = _unitOfWork.Categories.GetSelectList();
			return View(productVm);
		}

		[AuthorizePolicy(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Edit(int? id, ProductVM productVm)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (!ModelState.IsValid)
			{
				productVm.Categories = _unitOfWork.Categories.GetSelectList();

				return View(productVm);
			}

			var product = productVm.MapTo<Product>();
			_productService.Update(id.Value, product);

			return RedirectToAction(MVC.Product.Index());
		}

		[AuthorizePolicy(Roles = "Administrator")]
		public virtual ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			_productService.Remove(id.Value);
			Alert("Product Removed", NotificationType.success);

			return RedirectToAction(MVC.Product.Index());
		}
	}
}
