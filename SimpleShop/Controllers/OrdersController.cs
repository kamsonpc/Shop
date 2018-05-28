using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Controllers
{
	[Authorize]
    public class OrdersController : Controller
    {
	    private readonly IOrderService _orderService;
	    public OrdersController(IOrderService orderService)
	    {
		    _orderService = orderService;
	    }
	    public ActionResult Index()
		{
			var orders = _orderService.GetOrdersByUser(User.Identity.GetUserId());
			return View(orders);
		}
	}
}