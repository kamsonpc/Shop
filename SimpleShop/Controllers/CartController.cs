using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SimpleShop.Filters;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Controllers
{
	[AuthorizeCustom]
	public class CartController : BaseController
    {
	    private readonly ICartService _cartService;

	    public CartController(ICartService cartService)
	    {
		    _cartService = cartService;
	    }
        public ActionResult Index()
        {
	        var userId = User.Identity.GetUserId();
	        var items =_cartService.GetAll(userId);
            return View(items);
        }

		[HttpGet]
	    public ActionResult Add(int productId,int orderQuantity)
	    {
		    var userId = User.Identity.GetUserId();

			var cartItem = new CartVM
		    {
				ProductId = productId,
				ApplicationUserId = userId,
				OrderedQuantity = orderQuantity
		    };

			_cartService.Add(cartItem);

		    return RedirectToAction("Index");
	    }

	    [HttpGet]
	    public ActionResult Remove(int id)
	    {
			_cartService.Remove(id);

		    return RedirectToAction("Index");
	    }

	    public ActionResult Complete()
	    {
		    var userId = User.Identity.GetUserId();
		    var cartItemCount = _cartService.Counter(userId);

		    if (cartItemCount == 0) return RedirectToAction("Index", "Product");
			return View();
	    }

		[HttpPost]
	    public ActionResult Complete(ShippingVM shippingData)
		{

			var userId = User.Identity.GetUserId();

			if (!ModelState.IsValid) return View(shippingData);
			

			_cartService.Complete(userId,shippingData);
		    return RedirectToAction("Index","Orders");
	    }

	    public ActionResult Counter()
	    {
		    var userId = User.Identity.GetUserId();
		    var cartItemsCount = _cartService.Counter(userId);
		    return Json(cartItemsCount,JsonRequestBehavior.AllowGet);
	    }
	}
}