using SimpleShop.Data.Interfaces;
using SimpleShop.Data.Models.Roles;
using SimpleShop.Data.Services;
using SimpleShop.Filters;
using SimpleShop.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace SimpleShop.Areas.Admin.Controllers
{
    [AuthorizeCustom(RoleTypes.Administrator)]
    public partial class FileManagerController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilemanagerService _filemanagerService;

        public FileManagerController(IUnitOfWork unitOfWork, IFilemanagerService filemanagerService)
        {
            _unitOfWork = unitOfWork;
            _filemanagerService = filemanagerService;
        }

        public virtual ActionResult Index()
        {
            return RedirectToAction(MVC.Admin.FileManager.List());
        }

        public virtual ActionResult List()
        {
            return View(MVC.Admin.FileManager.Views.List);
        }

        public virtual JsonResult ReadFiles(string path = @"/")
        {
            var folders = _filemanagerService.GetFilesTree();
            if (path == "/")
            {
                var currentFolder = folders.Where(x => x.ParentId == null);
            }
            var currentDir = _filemanagerService.GetFilesTree();
            return Json(currentDir);
        }



    }
}
