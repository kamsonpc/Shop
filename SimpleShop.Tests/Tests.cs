using System.Web.Mvc;
using NSubstitute;
using SimpleShop.Controllers;
using SimpleShop.Interfaces.Repositories;
using Xunit;
using Assert = Xunit.Assert;

namespace SimpleShop.Tests
{

	public class Tests
	{

		[Fact]
		public void HomeController_ReturnsAViewBag()
		{
			var homeController = new HomeController();

			var homeAction = homeController.Contact() as ViewResult;
			var result = homeAction.ViewBag.Message;

			Assert.Equal("Your contact page.", result);

		}

	}
}
