using System.Collections.Generic;
using System.Data.Entity;
using SimpleShop.Interfaces.Repositories;
using SimpleShop.Models;

namespace SimpleShop.Repositories
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		public ProductRepository(DbContext context) : base(context)
		{ }

		public  IEnumerable<Product> GetByCategory(int id)
		{
			return Find(p => p.CategoryId == id);
		}

		public IEnumerable<Product> GetByPrice(int minPrice, int maxPrice)
		{
			return Find(p => (p.Price <= minPrice && p.Price >= maxPrice));
		}

		public ApplicationDbContext ApplicationDbContext
		{
			get { return Contex as ApplicationDbContext; }
		}
	}
}