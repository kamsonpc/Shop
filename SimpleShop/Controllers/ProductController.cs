using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using SimpleShop.Filters;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IOrderService _order;
		private readonly IProductService _product;
		private readonly ICategoryService _category;
		public ProductController(IProductService product, IOrderService order, ICategoryService category)
		{
			_product = product;
			_order = order;
			_category = category;
			
		}

		// GET: Product
		[AllowAnonymous]
		public ActionResult Index()
		{
			var products = _product.GetAll();
			var categories = _category.GetAll();

			CategoryProductVM categoryProducts = new CategoryProductVM
			{
				product = products,
				categories = categories
			};

			return View(categoryProducts);
		}

		// GET: Product/Details/5
		[AllowAnonymous]
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
		
			var product = _product.GetById(id.Value);

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
				Categories = _category.GetSelectList()
			};

			return View(productVm);
		}

		// POST: Product/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AuthorizeCustom(Roles = "Administrator")]
		public ActionResult Create(ProductVM productVm, HttpPostedFileBase file)
		{
			if (!ModelState.IsValid)
			{
				productVm.Categories = _category.GetSelectList();
				return View(productVm);
			}

			if (file.ContentLength > 0 && file.ContentLength < 327680 && file.ContentType.Contains("image"))
			{
				try
				{
					productVm.Img = _product.UploadImage(file);
					_product.AddNew(productVm);

					return RedirectToAction("Index");

				}
				catch
				{
					ViewBag.Message = "File upload failed!!";
					return View();
				}
			}

			ViewBag.Message = "File bad file type";
			productVm.Categories = _category.GetSelectList();
			return View(productVm);
		}

		[AuthorizeCustom(Roles = "Administrator")]
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var product = _product.GetById(id.Value);
			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			product.Categories = _category.GetSelectList();
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
				productVm.Categories = _category.GetSelectList();

				return View(productVm);

			}
			if (_product.Update(id.Value, productVm))
			{
				return RedirectToAction("Index");

			}
			else
			{
				return View(productVm);
			}

		}

		[AuthorizeCustom(Roles = "Administrator")]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			_product.Remove(id.Value);
			return RedirectToAction("Index");
		}

		[Authorize]
		public ActionResult Buy(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var shipping = _product.GetById(id.Value);
			if (shipping == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
			ShippingVM shippingVm = new ShippingVM
			{
				ProductId = shipping.ProductId
			};

			return View(shippingVm);

		}

		[Authorize]
		[HttpPost]
		public ActionResult Buy(int? id, ShippingVM shippingData)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var product = _product.GetById(id.Value);
			var user = User.Identity.GetUserId();
			if (product == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			if (ModelState.IsValid)
			{
				_product.ChangeQuantity(id.Value, 1);

				Order order = new Order
				{
					ApplicationUserId = user,
					ProductId = product.ProductId,
					Date = DateTime.Now,
					Price = product.Price,
					Quantity = 1,
					Payment = false,
					NameAndSurname = shippingData.NameAndSurname,
					PhoneNumber = shippingData.PhoneNumber,
					Address = shippingData.Address,
					CityCode = shippingData.CityCode,
					Country = shippingData.Country
				};


				_order.AddNew(order);
				return View("BuySuccess", product);

			}
			return RedirectToAction("Index");
		}

		[AllowAnonymous]
		public ActionResult Category(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var products = _product.GetByCategory(id.Value);
			var categories = _category.GetAll();

			CategoryProductVM categoryProducts = new CategoryProductVM
			{
				product = products,
				categories = categories
			};

			return View("Index", categoryProducts);
		}

		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Search(string name)
		{
			var products = _product.GetAll();
			var categories = _category.GetAll();

			if (name != "")
			{
				products = products.FindAll(m => m.Name.Contains(name));
			}

			CategoryProductVM categoryProducts = new CategoryProductVM
			{
				product = products,
				categories = categories
			};
			return View("Index", categoryProducts);
		}

	}
}
