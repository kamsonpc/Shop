﻿using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using PagedList;
using SimpleShop.Extensions;
using SimpleShop.Filters;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models.ViewsModels;
using SimpleShop.T4MVC;

namespace SimpleShop.Controllers
{
	[AuthorizePolicy(Roles = "Administrator")]
	public partial class PaymentController : BaseController
	{
		private readonly IOrderService _orderService;

		public PaymentController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public virtual ActionResult Index(string search, int? page)
		{
			var pageNumber = page ?? 1;
			var orders = string.IsNullOrEmpty(search) ? _orderService.GetAll() : _orderService.Find(search);

			var result = orders.MapTo<List<OrdersPageVM>>()
			   .ToPagedList(pageNumber, PageSize);

			ViewBag.Search = search;

			return View(result);
		}

		public virtual ActionResult Pay(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			_orderService.Pay(id.Value);
			return RedirectToAction(MVC.Payment.Index());

		}

		public virtual ActionResult Shipping(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var shippingData = _orderService.GetShippinDataById(id.Value).MapTo<ShippingVM>();
			if (shippingData == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			return View(shippingData);
		}
	}
}