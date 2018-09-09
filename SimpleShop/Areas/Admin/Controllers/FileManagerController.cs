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
    public partial class FileManagerController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FileManagerController(IUnitOfWork unitOfWork)
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
    }
		
}
