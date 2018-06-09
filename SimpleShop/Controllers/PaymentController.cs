using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using PagedList;
using SimpleShop.Filters;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Controllers
{ 
	[AuthorizeCustom(Roles = "Administrator")]
	public class PaymentController : BaseController
    {
	    private readonly IOrderRepository _order;

		private const int numberProductOnPage = 10;

		public PaymentController(IOrderRepository order)
		{
			_order = order;
		}
 
	    public ActionResult Index(string search,int? page)
	    {
			var pageNumber = page ?? 1;
			var orders = _order.GetAllOrders();
			if(!string.IsNullOrEmpty(search))
			{
				orders = orders.FindAll(s => (s.ApplicationUser.Email.Contains(search)) || (s.Product.Name.Contains(search)));
			}
			
			ViewBag.Search = search;
			
			return View(orders.ToPagedList(pageNumber, numberProductOnPage));
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

		    var shippingData = _order.GetShippingById(id.Value);
		    if (shippingData==null)
		    {
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

		    return View(shippingData);
	    }
	}
}