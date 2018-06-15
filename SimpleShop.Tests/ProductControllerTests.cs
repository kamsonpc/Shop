using System.Collections.Generic;
using System.Web.Mvc;
using NSubstitute;
using SimpleShop.Controllers;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models.ViewsModels;
using SimpleShop.Models.SearchModels;
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


			return new ProductController(mockProduct, mockCategory);
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

		[Fact]
		public void Filter_returns_full_list_when_categoryId_and_productSE_is_null()
		{
			var controller = Controller();

			int? categoryId = null;
			var ProductSH = new ProductSearchModel
			{
				Name = "",
				PriceFrom = null,
				PriceTo = null
			};

			var products = new List<ProductVM>();

			products.Add(new ProductVM { ProductId = 1, CategoryId = 1, CustomerQuantity = 10, Quantity = 30, Name = "Samsung S8 Plus", Price = 3555, Description = "Fajny Telefon", Img = "C://ProgramFiles/samsung.jpg" });
			products.Add(new ProductVM { ProductId = 2, CategoryId = 1, CustomerQuantity = 5, Quantity = 10, Name = "Samsung S7", Price = 1800, Description = "Przecietny Telefon", Img = "C://ProgramFiles/samsungs7.jpg" });
			products.Add(new ProductVM { ProductId = 3, CategoryId = 2, CustomerQuantity = 1, Quantity = 10, Name = "Nexus 7", Price = 1200, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" });

			var filter_result = controller.Filter(categoryId, ProductSH, products);

			Assert.Equal(products, filter_result);
		}


		[Fact]
		public void Filter_returns_only_product_with_categoryId_equal_1()
		{
			var controller = Controller();

			int? categoryId = 1;
			var ProductSH = new ProductSearchModel
			{
				Name = "",
				PriceFrom = null,
				PriceTo = null
			};

			var products = new List<ProductVM>();

			var product1 = new ProductVM { ProductId = 1, CategoryId = 1, CustomerQuantity = 10, Quantity = 30, Name = "Samsung S8 Plus", Price = 3555, Description = "Fajny Telefon", Img = "C://ProgramFiles/samsung.jpg" };
			var product2 = new ProductVM { ProductId = 2, CategoryId = 2, CustomerQuantity = 1, Quantity = 10, Name = "Nexus 7", Price = 1200, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };
			var product3 = new ProductVM { ProductId = 3, CategoryId = 1, CustomerQuantity = 2, Quantity = 10, Name = "Nexus 6", Price = 12200, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };


			products.Add(product1);
			products.Add(product2);
			products.Add(product3);

			var filter_result = controller.Filter(categoryId, ProductSH, products);
			var filter_expected = new List<ProductVM>();

			products.Remove(product2);

			Assert.Equal(products, filter_result);
		}



		[Fact]
		public void Filter_returns_only_product_with_price_from_100_to_1000()
		{
			var controller = Controller();

			int? categoryId = null;
			var ProductSH = new ProductSearchModel
			{
				Name = "",
				PriceFrom = 100,
				PriceTo = 1000
			};

			var products = new List<ProductVM>();

			var product1 = new ProductVM { ProductId = 1, CategoryId = 1, CustomerQuantity = 10, Quantity = 30, Name = "Samsung S8 Plus", Price = 100, Description = "Fajny Telefon", Img = "C://ProgramFiles/samsung.jpg" };
			var product2 = new ProductVM { ProductId = 2, CategoryId = 2, CustomerQuantity = 1, Quantity = 10, Name = "Nexus 7", Price = 1000, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };
			var product3 = new ProductVM { ProductId = 3, CategoryId = 1, CustomerQuantity = 2, Quantity = 10, Name = "Nexus 6", Price = 12200, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };
			var product4 = new ProductVM { ProductId = 4, CategoryId = 1, CustomerQuantity = 2, Quantity = 10, Name = "Nexus 6", Price = 99, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };
			var product5 = new ProductVM { ProductId = 5, CategoryId = 1, CustomerQuantity = 2, Quantity = 10, Name = "Nexus 6", Price = 1001, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };


			products.Add(product1);
			products.Add(product2);
			products.Add(product3);
			products.Add(product4);
			products.Add(product5);

			var filter_result = controller.Filter(categoryId, ProductSH, products);

			products.Remove(product3);
			products.Remove(product4);
			products.Remove(product5);

			Assert.Equal(products, filter_result);
		}



		[Fact]
		public void Filter_returns_only_product_with_name_equal_Samsung()
		{
			var controller = Controller();

			int? categoryId = null;
			var ProductSH = new ProductSearchModel
			{
				Name = "Samsung",
				PriceFrom = null,
				PriceTo = null
			};

			var products = new List<ProductVM>();

			var product1 = new ProductVM { ProductId = 1, CategoryId = 1, CustomerQuantity = 10, Quantity = 30, Name = "Samsung S8 Plus", Price = 100, Description = "Fajny Telefon", Img = "C://ProgramFiles/samsung.jpg" };
			var product2 = new ProductVM { ProductId = 2, CategoryId = 2, CustomerQuantity = 1, Quantity = 10, Name = "Samsung S7", Price = 1000, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };
			var product3 = new ProductVM { ProductId = 3, CategoryId = 2, CustomerQuantity = 1, Quantity = 10, Name = "Nexus 7", Price = 1000, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };


			products.Add(product1);
			products.Add(product2);
			products.Add(product3);


			var filter_result = controller.Filter(categoryId, ProductSH, products);

			products.Remove(product3);

			Assert.Equal(products, filter_result);
		}

		[Fact]
		public void Filter_returns_object_with_categoryId_Search_and_Price()
		{
			var controller = Controller();

			int? categoryId = 1;
			var ProductSH = new ProductSearchModel
			{
				Name = "Samsung",
				PriceFrom = 100,
				PriceTo = 1000
			};

			var products = new List<ProductVM>();

			var product1 = new ProductVM { ProductId = 1, CategoryId = 1, CustomerQuantity = 10, Quantity = 30, Name = "Samsung S8 Plus", Price = 2000, Description = "Fajny Telefon", Img = "C://ProgramFiles/samsung.jpg" };
			var product2 = new ProductVM { ProductId = 2, CategoryId = 1, CustomerQuantity = 1, Quantity = 10, Name = "Samsung S7", Price = 1000, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };
			var product3 = new ProductVM { ProductId = 3, CategoryId = 2, CustomerQuantity = 1, Quantity = 10, Name = "Samsung Galaxy Tab 2", Price = 580, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };
			var product4 = new ProductVM { ProductId = 4, CategoryId = 2, CustomerQuantity = 1, Quantity = 10, Name = "Nexus 7", Price = 2000, Description = "Przecietny Table", Img = "C://ProgramFiles/nexus.jpg" };


			products.Add(product1);
			products.Add(product2);
			products.Add(product3);


			var filter_result = controller.Filter(categoryId, ProductSH, products);

			products.Remove(product1);
			products.Remove(product3);
			products.Remove(product4);

			Assert.Equal(products, filter_result);
		}
	}
}

