using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Interfaces;
using SimpleShop.Models;

namespace SimpleShop.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class CategoriesController : Controller
    {

		private readonly ICategoryService _categoryService;
	    public CategoriesController(ICategoryService categoryService)
	    {
		    _categoryService = categoryService;
	    }

		// GET: Categories
		public ActionResult Index()
        {
            return View(_categoryService.GetAll());
        }


        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddNew(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = _categoryService.GetById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category,int id)
        {
            if (ModelState.IsValid)
            {
	            _categoryService.Update(id, category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {

			_categoryService.Remove(id);
	        return RedirectToAction("Index");
		}
    }
}
