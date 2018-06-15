using System.Net;
using System.Web.Mvc;
using SimpleShop.Filters;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models;

namespace SimpleShop.Controllers
{
	[AuthorizeCustom(Roles = "Administrator")]
	public class CategoriesController : BaseController
    {

		private readonly ICategoryService _categoryService;

	    public CategoriesController(ICategoryService categoryService)
	    {
		    _categoryService = categoryService;
	    }

	    public ActionResult Index()
        {
            return View(_categoryService.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

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

        public ActionResult Edit(int? id)
        {
	        if (id == null)
	        {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }
            Category category = _categoryService.GetById(id.Value);
            if (category == null)
            {
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category,int? id)
        {
	        if (id == null)
	        {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }
            if (ModelState.IsValid)
            {
	            //_categoryService.Update(id.Value, category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
	        if (id == null)
	        {
		        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }

			_categoryService.Remove(id.Value);
	        return RedirectToAction("Index");
		}
    }
}
