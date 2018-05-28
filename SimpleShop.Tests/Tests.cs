using System;
using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
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
			var mockService = Substitute.For<IProductService>();
			var homeController = new HomeController(mockService);

			var homeAction = homeController.Contact() as ViewResult;
			var result = homeAction.ViewBag.Message;

			Assert.Equal("Your contact page.", result);

		}

	}
}
