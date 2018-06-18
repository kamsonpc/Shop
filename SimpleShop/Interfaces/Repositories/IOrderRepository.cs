using System.Collections.Generic;
using SimpleShop.Models;

namespace SimpleShop.Interfaces.Repositories
{
	public interface IOrderRepository : IRepository<Order>
	{
		IEnumerable<Order> GetOrdersByUserId(string userId);
	}
}
