using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SimpleShop.Interfaces.Repositories;
using SimpleShop.Models;

namespace SimpleShop.Repositories
{
	public class OrderRepository : Repository<Order>,IOrderRepository
	{
		public OrderRepository(DbContext context) : base(context)
		{
		}

		public new IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate)
		{
			return ApplicationDbContext.Orders.Include(p => p.Product).Include(a => a.ApplicationUser).Where(predicate);
		}

		public IEnumerable<Order> GetOrdersByUserId(string userId)
		{
			return ApplicationDbContext.Orders.Include(p => p.Product).Where(p => p.ApplicationUser.Id == userId);
		}

		public new IEnumerable<Order> GetAll()
		{
			return ApplicationDbContext.Orders.Include(p => p.Product).Include(a => a.ApplicationUser);
		}

		public ApplicationDbContext ApplicationDbContext
		{
			get { return Contex as ApplicationDbContext; }
		}
	}
}