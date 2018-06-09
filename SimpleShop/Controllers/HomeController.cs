using System.Web.Mvc;
using SimpleShop.Interfaces;

namespace SimpleShop.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProductRepository _product;

		public HomeController(IProductRepository product)
		{
			_product = product;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}