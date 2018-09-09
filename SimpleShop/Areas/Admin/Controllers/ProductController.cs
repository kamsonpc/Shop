using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Areas.Admin.Models.Products;
using SimpleShop.Areas.Client.Models.Product;
using SimpleShop.Data.Extensions;
using SimpleShop.Data.Interfaces;
using SimpleShop.Data.Interfaces.Services;
using SimpleShop.Data.Models;
using SimpleShop.Filters;
using SimpleShop.Helpers;

namespace SimpleShop.Areas.Admin.Controllers
{
	[AuthorizeCustom(RoleTypes.Administrator)]
	[RouteArea("Admin", AreaPrefix = "admin")]

	public partial class ProductController : BaseController
	{
		private readonly IProductService _productService;
		private readonly IUnitOfWork _unitOfWork;

		public ProductController(IProductService productService, IUnitOfWork unitOfWork)
		{
			_productService = productService;
			_unitOfWork = unitOfWork;
		}

		private List<SelectListItem> PopulateCategoriesList()
		{
			return _unitOfWork.Categories.GetAll()
				.Select(x => new SelectListItem() { Value = x.CategoryId.ToString(), Text = x.Name })
				.ToList();
		}

		[Route("Products")]
		public virtual ActionResult Index()
		{
			var products = _unitOfWork.Products.GetAll().MapTo<IEnumerable<ProductVM>>();
			return View(MVC.Admin.Product.Views.ViewNames.List,products);
		}

		//		[AuthorizeCustom(Roles = "Administrator")]
		//		public virtual ActionResult Create()
		//		{
		//			var productVm = new ProductVM
		//			{
		//				Categories = PopulateCategoriesList()
		//			};

		//			return View(productVm);
		//		}

		//		[HttpPost]
		//		[ValidateAntiForgeryToken]
		//		public virtual ActionResult Create(ProductVM productVm, HttpPostedFileBase file)
		//		{
		//			productVm.Categories = PopulateCategoriesList();

		//			if (!ModelState.IsValid)
		//			{
		//				return View(productVm);
		//			}

		//			if (file.ContentLength > 0 && file.ContentLength < 327680 && file.ContentType.Contains("image"))
		//			{
		//				try
		//				{
		//					productVm.Img = _productService.UploadImage(file);
		//					var product = productVm.MapTo<Product>();
		//					_productService.AddNew(product);
		//					Alert("Dodano produkt : " + product.Name, NotificationType.success);
		//					return RedirectToAction(MVC.Client.Home.Index());
		//				}
		//				catch (Exception e)
		//				{
		//					Console.WriteLine(e);
		//					Alert("Nie udało się dodać", NotificationType.danger);
		//					return View(productVm);
		//				}
		//			}

		//			Alert("Invalid Image", NotificationType.danger);
		//			return View(productVm);

		//		}

		//		public virtual ActionResult Edit(int? id)
		//		{
		//			if (id == null)
		//			{
		//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//			}
		//			var productVm = _productService.GetById(id.Value).MapTo<ProductVM>();
		//			if (productVm == null)
		//			{
		//				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
		//			}

		//			productVm.Categories = PopulateCategoriesList();
		//			return View(productVm);
		//		}

		//		[HttpPost]
		//		[ValidateAntiForgeryToken]
		//		public virtual ActionResult Edit(int? id, ProductVM productVm)
		//		{
		//			if (id == null)
		//			{
		//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//			}
		//			if (!ModelState.IsValid)
		//			{
		//				productVm.Categories = PopulateCategoriesList();

		//				return View(productVm);
		//			}

		//			var product = productVm.MapTo<Product>();
		//			_productService.Update(id.Value, product);

		//			return RedirectToAction(MVC.Client.Home.Index());
		//		}

		//		public virtual ActionResult Delete(int? id)
		//		{
		//			if (id == null)
		//			{
		//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//			}

		//			_productService.Remove(id.Value);
		//			Alert("Product Removed", NotificationType.success);

		//			return RedirectToAction(MVC.Client.Home.Index());
		//		}
	}
}
