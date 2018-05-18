﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
			var result = Mapper.Map<List<Order>, List<OrderProductUserVM>>(orders);
			return View(result);
		}
	}
}