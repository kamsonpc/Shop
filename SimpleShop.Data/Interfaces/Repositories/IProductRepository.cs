using System.Collections.Generic;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.SearchModels;

namespace SimpleShop.Data.Interfaces.Repositories
{
	public interface IProductRepository : IRepository<Product>
	{
		IEnumerable<Product> GetByCategory(int id);
		IEnumerable<Product> GetByPrice(int minPrice, int maxPrice);
		IEnumerable<Product> Search(int? categoryId,ProductSearchModel searchModel);
		void Update(int id, Product product);

	}
}
