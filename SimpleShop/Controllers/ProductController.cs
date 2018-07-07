using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SimpleShop.Filters;
using SimpleShop.Interfaces;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models.SearchModels;
using SimpleShop.Models.ViewsModels;
using static SimpleShop.Views.Shared.Alerts.Alert;

namespace SimpleShop.Controllers
{
	[Authorize]
	public partial class ProductController : BaseController
	{
		private readonly IProductService _productService;
		private readonly IUnitOfWork _unitOfWork;
		private const int NumberProductOnPage = 9;

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
				Product = _productService.Search(search, categoryId).ToPagedList(pageNumber, NumberProductOnPage),
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

			var product = _productService.GetById(id.Value);

			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			return View(product);
		}

		[AuthorizeCustom(Roles = "Administrator")]
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
		[AuthorizeCustom(Roles = "Administrator")]
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
					_productService.AddNew(productVm);
					Alert("Dodano produkt : " + productVm.Name, NotificationType.success);
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

		[AuthorizeCustom(Roles = "Administrator")]
		public virtual ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var product = _productService.GetById(id.Value);
			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			product.Categories = _unitOfWork.Categories.GetSelectList();
			return View(product);
		}

		[AuthorizeCustom(Roles = "Administrator")]
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

			_productService.Update(id.Value, productVm);

			return RedirectToAction(MVC.Product.Index());
		}

		[AuthorizeCustom(Roles = "Administrator")]
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
