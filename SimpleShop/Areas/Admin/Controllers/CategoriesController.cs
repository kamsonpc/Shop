using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SimpleShop.Areas.Admin.Models.Categories;
using SimpleShop.Data.Extensions;
using SimpleShop.Data.Interfaces;
using SimpleShop.Data.Models;
using SimpleShop.Filters;
using SimpleShop.Helpers;

namespace SimpleShop.Areas.Admin.Controllers
{
	[AuthorizeCustom(RoleTypes.Administrator)]
	public partial class CategoriesController : BaseController
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoriesController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public virtual ActionResult Index()
		{
			var categories = _unitOfWork.Categories.GetAll()
				.ToList()
				.MapTo<IEnumerable<CategoryViewModel>>();
			return View(categories);
		}

		public virtual ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Create(Category category)
		{
			if (!ModelState.IsValid) return View(category);
			_unitOfWork.Categories.Add(category);
			_unitOfWork.Complete();
			return RedirectToAction(MVC.Admin.Categories.Index());

		}

		public virtual ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var category = _unitOfWork.Categories.Get(id.Value);
			if (category == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			return View(category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Edit(Category category, int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (!ModelState.IsValid) return View(category);
			_unitOfWork.Categories.Update(category, id.Value);
			_unitOfWork.Complete();
			return RedirectToAction(MVC.Admin.Categories.Index());
		}

		public virtual ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var categoryToRemove = _unitOfWork.Categories.Get(id.Value);
			_unitOfWork.Categories.Remove(categoryToRemove);
			_unitOfWork.Complete();
			return RedirectToAction(MVC.Admin.Categories.Index());
		}
	}
}
