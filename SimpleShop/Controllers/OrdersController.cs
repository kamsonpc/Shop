using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Controllers
{
	[Authorize]
    public class OrdersController : BaseController
    {
	    private readonly IOrderService _orderService;

		private int pageSize = 10;

	    public OrdersController(IOrderService orderService)
	    {
		    _orderService = orderService;
	    }
	    public ActionResult Index(int? page)
		{
			var pageNumber = page ?? 1;
			var orders = _orderService.GetOrdersByUser(User.Identity.GetUserId()).ToPagedList(pageNumber,pageSize);
			return View(orders);
		}
	}
}