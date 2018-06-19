using System.Collections.Generic;
using System.Web.Mvc;
using NSubstitute;
using SimpleShop.Controllers;
using SimpleShop.Interfaces;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models.ViewsModels;
using SimpleShop.Models.SearchModels;
using Xunit;
using PagedList;


namespace SimpleShop.Tests
{
	public class ProductControllerTests
	{
		[Fact]
		public void Filter_returns_only_product_with_name_equal_Samsung()
		{
			int? categoryId = null;
			var ProductSH = new ProductSearchModel
			{
				Name = "Samsung",
				PriceFrom = null,
				PriceTo = null
			};



			var product1 = new ProductVM { ProductId = 1, CategoryId = 1, CustomerQuantity = 10, Quantity = 30, Name = "Samsung S8 Plus", Price = 100, Description = "Fajny Telefon", Img = "C://ProgramFiles/samsung.jpg" };
			var product2 = new ProductVM { ProductId = 2, CategoryId = 2, CustomerQuantity = 1, Quantity = 10, Name = "Samsung S7", Price = 1000, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };
			var product3 = new ProductVM { ProductId = 3, CategoryId = 2, CustomerQuantity = 1, Quantity = 10, Name = "Nexus 7", Price = 1000, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };

			var products = new List<ProductVM>
			{
				product1,product2,product3
			};

			

			var mockUnitOfWork = Substitute.For<IUnitOfWork>();
			var productServiceMoq = Substitute.For<IProductService>();
			

			var controller = new ProductController(productServiceMoq, mockUnitOfWork);

			productServiceMoq.Search(ProductSH,categoryId).Returns(products);

			var filter_result =controller.Index(categoryId,ProductSH,1) as ViewResult;
			var result = (ProductPageVM)filter_result.ViewData.Model;


			products.Remove(product3);
			var expected = products.ToPagedList(1, 9);

			Assert.Equal(expected,result.Product);
		}
	}
}

