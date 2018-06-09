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
	    private readonly IOrderRepository _orderService;

		private const int numberProductOnPage = 10;

	    public OrdersController(IOrderRepository orderService)
	    {
		    _orderService = orderService;
	    }
	    public ActionResult Index(int? page)
		{
			var pageNumber = page ?? 1;
			var orders = _orderService.GetOrdersByUser(User.Identity.GetUserId()).ToPagedList(pageNumber, numberProductOnPage);
			return View(orders);
		}
	}
}