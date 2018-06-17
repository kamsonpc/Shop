using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SimpleShop.Interfaces.Repositories;
using SimpleShop.Models;
using SimpleShop.Models.SearchModels;

namespace SimpleShop.Repositories
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		public ProductRepository(DbContext context) : base(context)
		{ }

		public IEnumerable<Product> GetByCategory(int id)
		{
			return Find(p => p.CategoryId == id);
		}

		public IEnumerable<Product> GetByPrice(int minPrice, int maxPrice)
		{
			return Find(p => (p.Price <= minPrice && p.Price >= maxPrice));
		}

		public IEnumerable<Product> Search(int? categoryId, ProductSearchModel searchModel)
		{
			var query = ApplicationDbContext.Products.AsQueryable();
			if (categoryId != null)
			{
				query = query.Where(p => p.CategoryId == categoryId);
			}

			if (!string.IsNullOrEmpty(searchModel.Name))
			{
				query = query.Where(p => p.Name.Contains(searchModel.Name));
			}

			if (searchModel.PriceFrom != null && searchModel.PriceTo != null)
			{
				query = query.Where(p => (p.Price >= searchModel.PriceFrom && p.Price <= searchModel.PriceTo));
			}

			return query.ToList();
		}

		public void Update(int id, Product product)
		{
			var productInDb = Get(id);
			if (productInDb == null) return;
			productInDb.Name = product.Name;
			productInDb.CategoryId = product.CategoryId;
			productInDb.Price = product.Price;
			productInDb.Quantity = product.Quantity;

		}

		public ApplicationDbContext ApplicationDbContext
		{
			get { return Contex as ApplicationDbContext; }
		}
	}
}