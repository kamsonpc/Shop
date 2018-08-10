using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SimpleShop.Areas.Admin.Models.Categories;
using SimpleShop.Data.Extensions;
using SimpleShop.Data.Interfaces;
using SimpleShop.Data.Interfaces.Services;
using SimpleShop.Data.Models;
using SimpleShop.Filters;
using SimpleShop.Helpers;

namespace SimpleShop.Areas.Admin.Controllers
{
	[AuthorizeCustom(Roles = RolesTypes.Administrator)]
	public partial class CategoriesController : BaseController
	{
		private readonly ICategoriesService _categoriesService;

		public CategoriesController(ICategoriesService categoriesService)
		{
			_categoriesService = categoriesService;
		}

		public virtual ActionResult Index()
		{
			var categories = _categoriesService.GetAll()
				.MapTo<IEnumerable<CategoryViewModel>>();
			return View(MVC.Admin.Categories.Views.Index,categories);
		}

		public virtual ActionResult Create()
		{
			return View(MVC.Admin.Categories.Views.Create);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Create(Category category)
		{
			if (!ModelState.IsValid) return View(category);
			_categoriesService.Add(category.MapTo<Category>());
			return RedirectToAction(MVC.Admin.Categories.Index());

		}

		public virtual ActionResult Edit(int id)
		{
			var category = _categoriesService.GetById(id)
				.MapTo<CategoryViewModel>();
			return View(MVC.Admin.Categories.Views.Edit,category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Edit(CategoryViewModel category)
		{

			if (!ModelState.IsValid) return View(MVC.Admin.Categories.Views.Edit, category);
			_categoriesService.Update(category.MapTo<Category>());
			return RedirectToAction(MVC.Admin.Categories.Index());
		}

		public virtual ActionResult Delete(int id)
		{
			_categoriesService.Remove(id);
			return RedirectToAction(MVC.Admin.Categories.Index());
		}
	}
}
