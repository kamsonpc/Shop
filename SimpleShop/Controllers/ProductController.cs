using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SimpleShop.Filters;
using SimpleShop.Interfaces;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models.SearchModels;
using SimpleShop.Models.ViewsModels;
using static SimpleShop.Views.Shared.Alerts.Alert;

namespace SimpleShop.Controllers
{
	[Authorize]
	public class ProductController : BaseController
	{
		private readonly IProductService _productService;
		private readonly IUnitOfWork _unitOfWork;
		private const int NumberProductOnPage = 9;

		public ProductController(IProductService productServiceService,IUnitOfWork unitOfWork)
		{
			_productService = productServiceService;
			_unitOfWork = unitOfWork;
		}
		
	

		[AllowAnonymous]
		public ActionResult Index(int? categoryId,ProductSearchModel search, int? page)
		{
			var pageNumber = page ?? 1;

			ViewBag.Search = search;
			ViewBag.CategoryId = categoryId;

			var categories = _unitOfWork.Categories.GetAll().ToList();
			var products = _productService.GetAll();

			var productPageVm = new ProductPageVM
			{
				Product = _productService.Search(search,categoryId).ToPagedList(pageNumber,NumberProductOnPage),
				Categories = categories,
				Search = search
			};

			return View(productPageVm);
		}


		[AllowAnonymous]
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var product = _productService.GetById(id.Value);

			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			return View(product);
		}

		[AuthorizeCustom(Roles = "Administrator")]
		public ActionResult Create()
		{
			ProductVM productVm = new ProductVM
			{
				Categories = _unitOfWork.Categories.GetSelectList()
			};

			return View(productVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[AuthorizeCustom(Roles = "Administrator")]
		public ActionResult Create(ProductVM productVm, HttpPostedFileBase file)
		{
			if (!ModelState.IsValid)
			{
				productVm.Categories = _unitOfWork.Categories.GetSelectList();
				return View(productVm);
			}

			if (file.ContentLength > 0 && file.ContentLength < 327680 && file.ContentType.Contains("image"))
			{
				try
				{
					productVm.Img = _productService.UploadImage(file);
					_productService.AddNew(productVm);
					Alert("Dodano produkt" + productVm.Name, NotificationType.success);

					return RedirectToAction("Index");
				}
				catch
				{
					Alert("Wysyłanie pliku nie powiodło się", NotificationType.danger);
					return View();
				}
			}

			Alert("Plik jest nie prawidłowy", NotificationType.danger);
			productVm.Categories = _unitOfWork.Categories.GetSelectList();
			return View(productVm);
		}

		[AuthorizeCustom(Roles = "Administrator")]
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var product = _productService.GetById(id.Value);
			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			product.Categories = _unitOfWork.Categories.GetSelectList();
			return View(product);
		}

		[AuthorizeCustom(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int? id, ProductVM productVm)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (!ModelState.IsValid)
			{
				productVm.Categories = _unitOfWork.Categories.GetSelectList();

				return View(productVm);
			}

			_productService.Update(id.Value, productVm);

			return RedirectToAction("Index");
		}

		[AuthorizeCustom(Roles = "Administrator")]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			_productService.Remove(id.Value);

			return RedirectToAction("Index");
		}
	}
}
