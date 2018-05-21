using System;
using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleShop.Controllers;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace SimpleShop.Tests
{
	
	public class Tests
	{

		[Fact]
		public void HomeController_ReturnsAViewBag()
		{
			var mockService = new Mock<IProductService>();
			var homeController = new HomeController(mockService.Object);

			var homeAction = homeController.Contact() as ViewResult;
			var result = homeAction.ViewBag.Message;

			Assert.Equal("Your contact page.", result);

		}
		[Fact]
		public void Details_Products_return_status_400_with_id_equal_null()
		{
			var mockProduct = new Mock<IProductService>();
			var mockOrder = new Mock<IOrderService>();
			var mockCategory = new Mock<ICategoryService>();

			var controller = new ProductController(mockProduct.Object,mockOrder.Object,mockCategory.Object);
			var result = controller.Details(null) as HttpStatusCodeResult;			

			Assert.Equal(400,result.StatusCode);
		}

		[Fact]
		public void Buy_Products_return_status_400_with_id_equal_null()
		{
			var mockProduct = new Mock<IProductService>();
			var mockOrder = new Mock<IOrderService>();
			var mockCategory = new Mock<ICategoryService>();

			var controller = new ProductController(mockProduct.Object, mockOrder.Object, mockCategory.Object);
			var result = controller.Buy(null) as HttpStatusCodeResult;

			Assert.Equal(400, result.StatusCode);
		}

		[Fact]
		public void Edit_Products_return_status_400_with_id_equal_null()
		{
			var mockProduct = new Mock<IProductService>();
			var mockOrder = new Mock<IOrderService>();
			var mockCategory = new Mock<ICategoryService>();

			var controller = new ProductController(mockProduct.Object, mockOrder.Object, mockCategory.Object);
			var result = controller.Edit(null) as HttpStatusCodeResult;

			Assert.Equal(400, result.StatusCode);
		}

		[Fact]
		public void Category_Products_return_status_400_with_id_equal_null()
		{
			var mockProduct = new Mock<IProductService>();
			var mockOrder = new Mock<IOrderService>();
			var mockCategory = new Mock<ICategoryService>();

			var controller = new ProductController(mockProduct.Object, mockOrder.Object, mockCategory.Object);
			var result = controller.Category(null) as HttpStatusCodeResult;

			Assert.Equal(400, result.StatusCode);
		}

		[Fact]
		public void Delete_Products_return_status_400_with_id_equal_null()
		{
			var mockProduct = new Mock<IProductService>();
			var mockOrder = new Mock<IOrderService>();
			var mockCategory = new Mock<ICategoryService>();

			var controller = new ProductController(mockProduct.Object, mockOrder.Object, mockCategory.Object);
			var result = controller.Delete(null) as HttpStatusCodeResult;

			Assert.Equal(400, result.StatusCode);
		}

		[Fact]
		public void Details_Products_not_Found_Id_404()
		{
			var mockProduct = new Mock<IProductService>();
			var mockOrder = new Mock<IOrderService>();
			var mockCategory = new Mock<ICategoryService>();

			var controller = new ProductController(mockProduct.Object, mockOrder.Object, mockCategory.Object);
			var result = controller.Details(-10) as HttpStatusCodeResult;

			Assert.Equal(404, result.StatusCode);
		}
	}
}
