using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class PaymentController : Controller
    {
	    private readonly IOrderService _order;
		public PaymentController(IOrderService order)
		{
			_order = order;
		}
 
	    public ActionResult Index()
	    {
		    var orders = _order.GetAllOrders();
		    var result = Mapper.Map<List<Order>, List<OrderProductUserVM>>(orders);
		    return View(result);
	    }
		[HttpPost]
	    public ActionResult Search(string name)
	    {
		    var orders = _order.GetAllOrders();
		    if (name != "")
		    {
			   orders = orders.FindAll(m => m.ApplicationUser.UserName.Contains(name));
		    }
		    var result = Mapper.Map<List<Order>, List<OrderProductUserVM>>(orders);
			return View("Index",result);
	    }

	    public ActionResult Change(int id)
		{
			_order.ChangePayment(id);
			return RedirectToAction("Index");
		}
	}
}