using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SimpleShop.Data.Interfaces.Repositories;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.Orders;

namespace SimpleShop.Data.Repositories
{
	public class OrderRepository : Repository<Order>,IOrderRepository
	{
		public OrderRepository(DbContext context) : base(context)
		{
		}

		public new IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate)
		{
			return _contex.Orders.Include(p => p.Product).Include(a => a.ApplicationUser).Where(predicate);
		}

        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public ApplicationDbContext _contex
		{
			get { return Contex as ApplicationDbContext; }
		}
	}
}