using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SimpleShop.Interfaces;
using SimpleShop.Models;

namespace SimpleShop.Services
{
	public class OrderService : IOrderService
	{
		private readonly ApplicationDbContext _applicationDb;
		public OrderService(ApplicationDbContext applicationDb)
		{
			_applicationDb = applicationDb;
		}

		public void AddNew(Order order)
		{
			_applicationDb.Orders.Add(order);
			_applicationDb.SaveChanges();
		}

		public Order GetDetails(int id)
		{
			var order = _applicationDb.Orders.SingleOrDefault(o => o.OrderId == id);
			return order;
		}

		public List<Order> GetOrdersByUser(string id)
		{
			var orders = _applicationDb.Orders.Where(b => b.ApplicationUserId == id).Include(m => m.Product).ToList();
			return orders;

		}

	}
}