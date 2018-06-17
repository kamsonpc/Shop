using System.Linq;
using System.Net;
using System.Web.Mvc;
using SimpleShop.Filters;
using SimpleShop.Interfaces;
using SimpleShop.Models;

namespace SimpleShop.Controllers
{
	[AuthorizeCustom(Roles = "Administrator")]
	public class CategoriesController : BaseController
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoriesController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

	    public ActionResult Index()
        {
            return View(_unitOfWork.Categories.GetAll().ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
	        if (!ModelState.IsValid) return View(category);
	        _unitOfWork.Categories.Add(category);
	        _unitOfWork.Complete();
	        return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
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
        public ActionResult Edit(Category category,int? id)
        {
	        if (id == null)
	        {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }

	        if (!ModelState.IsValid) return View(category);
	        _unitOfWork.Categories.Update(category,id.Value);
	        _unitOfWork.Complete();
	        return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
	        if (id == null)
	        {
		        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }

	        var categoryToRemove = _unitOfWork.Categories.Get(id.Value);
			_unitOfWork.Categories.Remove(categoryToRemove);
	        _unitOfWork.Complete();
	        return RedirectToAction("Index");
		}
    }
}
