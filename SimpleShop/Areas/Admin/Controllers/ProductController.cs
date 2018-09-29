using SimpleShop.Areas.Admin.Models.Products;
using SimpleShop.Data.Extensions;
using SimpleShop.Data.Interfaces.Services;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.Roles;
using SimpleShop.Filters;
using SimpleShop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SimpleShop.Areas.Admin.Controllers
{
	[AuthorizeCustom(RoleTypes.Administrator)]

	public partial class ProductController : BaseController
	{
		private readonly IProductService _productService;
		private readonly ICategoriesService _categoriesService;

		public ProductController(IProductService productService, ICategoriesService categoriesService)
		{
			_productService = productService;
			_categoriesService = categoriesService;
		}

		private List<SelectListItem> PopulateCategoriesList()
		{
			return _categoriesService.GetAll()
				.Select(x => new SelectListItem()
				{
					Value = x.CategoryId.ToString(),
					Text = x.Name
				})
				.ToList();
		}

		public virtual ActionResult Index()
		{
			var products = _productService.GetAll()
				.MapTo<IEnumerable<ProductListViewModel>>();
			return View(MVC.Admin.Product.Views.ViewNames.List, products);
		}

		[AuthorizeCustom(RoleTypes.Administrator)]
		public virtual ActionResult Create()
		{
			var productVm = new ProductViewModel()
			{
				Categories = PopulateCategoriesList()
			};

			return View(MVC.Admin.Product.Views.Create, productVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Create(ProductViewModel productVm, HttpPostedFileBase file)
		{
			productVm.Categories = PopulateCategoriesList();

			if (!ModelState.IsValid)
			{
				return View(MVC.Admin.Product.Views.Create, productVm);
			}

			if (file.ContentLength > 0 && file.ContentLength < 327680 && file.ContentType.Contains("image"))
			{
				try
				{
					productVm.Img = _productService.UploadImage(file);

					var product = productVm.MapTo<Product>();
					_productService.AddNew(product);

					Alert("Dodano produkt : " + product.Name, NotificationType.success);
					return RedirectToAction(MVC.Client.Home.Index());
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					Alert("Nie udało się dodać", NotificationType.danger);
					return View(MVC.Admin.Product.Views.Create, productVm);
				}
			}

			Alert("Invalid Image", NotificationType.danger);
			return View(productVm);

		}

		public virtual ActionResult Edit(int id)
		{
			var productVm = _productService.GetById(id).MapTo<ProductViewModel>();
			if (productVm == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			productVm.Categories = PopulateCategoriesList();
			return View(MVC.Admin.Product.Views.Edit, productVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Edit(int id, ProductViewModel productVm)
		{

			if (!ModelState.IsValid)
			{
				productVm.Categories = PopulateCategoriesList();

				return View(MVC.Admin.Product.Views.Edit, productVm);
			}

			var product = productVm.MapTo<Product>();
			_productService.Update(id, product);

			return RedirectToAction(MVC.Client.Home.Index());
		}

		public virtual ActionResult Delete(int id)
		{
			_productService.Remove(id);
			Alert("Product Removed", NotificationType.success);

			return RedirectToAction(MVC.Client.Home.Index());
		}
	}
}
