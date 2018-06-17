using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SimpleShop.Interfaces.Repositories;
using SimpleShop.Models;

namespace SimpleShop.Repositories
{
	public class OrderRepository : Repository<Order>,IOrderRepository
	{
		public OrderRepository(DbContext context) : base(context)
		{
		}

		public IEnumerable<Order> GetOrdersByProductName(string name)
		{
			return GetAll().Where( p => p.Product.Name == name);
		}

		public IEnumerable<Order> GetOrdersByUserId(string userId)
		{
			return GetAll().Where(p => p.ApplicationUser.Id == userId);
		}

		public ApplicationDbContext ApplicationDbContext
		{
			get { return Contex as ApplicationDbContext; }
		}
	}
}