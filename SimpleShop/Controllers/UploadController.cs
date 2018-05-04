using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Interfaces;
using SimpleShop.Models;

namespace SimpleShop.Controllers
{
    public class UploadController : Controller
    {
	    private readonly IUploadService _uploadService;
	    public UploadController(IUploadService uploadService)
	    {
		    _uploadService = uploadService;
	    }

        public ActionResult UploadImage()
        {
            return View();
        }

	    [HttpPost]
	    public ActionResult UploadImage(HttpPostedFileBase file)
	    {
		    try

		    {
			    if (file.ContentLength > 0 && file.ContentLength < 32768 && file.ContentType.Contains("image"))
			    {
				    string fileName = Path.GetFileName(file.FileName);
				    string path = Path.Combine(Server.MapPath("~/UploadedFiles"),fileName);
				    file.SaveAs(path);

				    Image image = new Image();
				    image.ImgPath = path;
				    _uploadService.UploadImage(image);
				    ViewBag.Message = "File Uploaded Successfully!!";
				    return View();
				}
			    ViewBag.Message = "File type is not correct !!";
			    return View();
			}
			catch
		    {
			    ViewBag.Message = "File upload failed!!";
			    return View();
		    }

	    }

	    public ActionResult Remove(int id)
	    {
		    _uploadService.RemoveImage(id);
		    return RedirectToAction("Index", "Home");
	    }
	}
    
}
