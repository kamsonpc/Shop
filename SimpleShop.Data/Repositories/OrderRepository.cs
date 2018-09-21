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
			return _contex.Orders.Where(predicate);
		}

        public IEnumerable<OrderInfo> GetOrdersByUserId(string userId)
        {

			return _contex.Orders.Where(x => x.ApplicationUserId == userId)
				.Join(_contex.Products, x => x.ProductId, o => o.ProductId, (o, x) =>
			        new OrderInfo
			        {
				        Id = o.OrderId,
				        Quantity = o.Quantity,
				        Date = o.Date,
				        Price = o.Price,
				        ProductName = x.Name
			        });

        }

        public ApplicationDbContext _contex
		{
			get { return Contex as ApplicationDbContext; }
		}
	}
}