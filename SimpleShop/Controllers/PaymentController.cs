using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using SimpleShop.Filters;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Controllers
{
	[AuthorizeCustom(Roles = "Administrator")]
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

	    public ActionResult Change(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (_order.ChangePayment(id.Value))
			{
				return RedirectToAction("Index");
			}
			return new HttpStatusCodeResult(HttpStatusCode.NotFound);

		}

	    public ActionResult Shipping(int? id)
	    {
		    if (id == null)
		    {
			    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		    }

		    var shippingData = Mapper.Map<Order, ShippingVM>(_order.GetById(id.Value));
		    if (shippingData==null)
		    {
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

		    return View(shippingData);
	    }
	}
}