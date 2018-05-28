using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NSubstitute;
using SimpleShop.Controllers;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;
using Xunit;

namespace SimpleShop.Tests
{
	public class ProductControllerTests
	{
		public ProductController Controller()
		{
			var mockProduct = Substitute.For<IProductService>();
			var mockOrder = Substitute.For<IOrderService>();
			var mockCategory = Substitute.For<ICategoryService>();


			return new ProductController(mockProduct, mockOrder, mockCategory);
		}
		[Fact]
		public void Details_when_id_equal_null_returns_status_code_400()
		{
			var controller = Controller();
			var result = controller.Details(null) as HttpStatusCodeResult;

			Assert.Equal(400, result.StatusCode);
		}

		[Fact]
		public void Details_when_cant_find_product_with_id_returns_status_code_404()
		{
			var controller = Controller();

			var result = controller.Details(-1) as HttpStatusCodeResult;
			Assert.Equal(404, result.StatusCode);
		}


	}
}
