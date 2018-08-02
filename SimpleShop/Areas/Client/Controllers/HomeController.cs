using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using SimpleShop.Areas.Admin.Models.Products;
using SimpleShop.Areas.Client.Models.Product;
using SimpleShop.Data.Extensions;
using SimpleShop.Data.Interfaces;
using SimpleShop.Data.Interfaces.Services;
using SimpleShop.Data.Models.SearchModels;
using SimpleShop.Helpers;

namespace SimpleShop.Areas.Client.Controllers
{
	public partial class HomeController : BaseController
	{
		private readonly IProductService _productService;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(IProductService productService, IUnitOfWork unitOfWork)
		{
			_productService = productService;
			_unitOfWork = unitOfWork;
		}



		public virtual ActionResult Index(int? page)
		{
			var pageNumber = page ?? 1;

			var categories = _unitOfWork.Categories.GetAll().ToList();

			var productPageVm = new ProductPageVM
			{
				Product = _productService.GetAll().MapTo<List<ProductVM>>()
					.ToPagedList(pageNumber, pageSize),
				Categories = categories,
			};

			return View(MVC.Client.Home.Views.ViewNames.Index, productPageVm);
		}


		//public virtual ActionResult Index(int? categoryId, ProductSearchModel search, int? page)
		//{
		//	var pageNumber = page ?? 1;

		//	ViewBag.Search = search;
		//	ViewBag.CategoryId = categoryId;

		//	var categories = _unitOfWork.Categories.GetAll().ToList();

		//	var productPageVm = new ProductPageVM
		//	{
		//		Product = _productService.Search(search, categoryId).MapTo<List<ProductVM>>()
		//			.ToPagedList(pageNumber, pageSize),
		//		Categories = categories,
		//		Search = search
		//	};

		//	return View(MVC.Client.Home.ActionNames.Index,productPageVm);
		//}


		public virtual ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var product = _productService.GetById(id.Value).MapTo<ProductVM>();

			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			return View(product);
		}


		public virtual ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public virtual ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}