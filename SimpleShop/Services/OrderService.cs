using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SimpleShop.Interfaces;
using SimpleShop.Models;

namespace SimpleShop.Services
{
	public class OrderService : IOrderService
	{
		private readonly IApplicationDbContex _applicationDb;
		public OrderService(IApplicationDbContex applicationDb)
		{
			_applicationDb = applicationDb;
		}

		public List<Order> GetAll()
		{
			return _applicationDb.Orders.Include(m => m.Product).ToList();
		}

		public void AddNew(Order order)
		{
			_applicationDb.Orders.Add(order);
			_applicationDb.SaveChanges();
		}

		public Order GetById(int id)
		{
			var order = _applicationDb.Orders.SingleOrDefault(o => o.OrderId == id);
			return order;
		}

		public List<Order> GetOrdersByUser(string id)
		{
			var orders = _applicationDb.Orders.Where(b => b.ApplicationUserId == id).Include(m => m.Product).ToList();
			return orders;

		}

		public List<Order> GetAllOrders()
		{
			var orders = _applicationDb.Orders.Include(m => m.Product).Include(x => x.ApplicationUser).OrderBy(d => d.Date).ToList();
			return orders;

		}

		public bool ChangePayment(int id)
		{
			var order = GetById(id);
			if (order != null)
			{
				if (order.Payment)
				{
					order.Payment = false;
				}
				else
				{
					order.Payment = true;
				}
				_applicationDb.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}