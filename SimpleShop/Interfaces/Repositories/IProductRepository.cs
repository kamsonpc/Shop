using System.Collections.Generic;
using SimpleShop.Models;

namespace SimpleShop.Interfaces.Repositories
{
	public interface IProductRepository : IRepository<Product>
	{
		IEnumerable<Product> GetByCategory(int id);
		IEnumerable<Product> GetByPrice(int minPrice, int maxPrice);
	}
}
