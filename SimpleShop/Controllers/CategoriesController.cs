﻿using System.Linq;
using System.Net;
using System.Web.Mvc;
using SimpleShop.Filters;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.T4MVC;

namespace SimpleShop.Controllers
{
	[AuthorizePolicy(Roles = "Administrator")]
	public partial class CategoriesController : BaseController
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoriesController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public virtual ActionResult Index()
		{
			return View(_unitOfWork.Categories.GetAll().ToList());
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
			return RedirectToAction(MVC.Categories.Index());

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
			return RedirectToAction(MVC.Categories.Index());
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
			return RedirectToAction(MVC.Categories.Index());
		}
	}
}
