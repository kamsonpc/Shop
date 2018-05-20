using System.Net;
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
        public ActionResult Edit(int? id)
        {
	        if (id == null)
	        {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }
            Category category = _categoryService.GetById(id.Value);
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
        public ActionResult Edit(Category category,int? id)
        {
	        if (id == null)
	        {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }
            if (ModelState.IsValid)
            {
	            _categoryService.Update(id.Value, category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
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
