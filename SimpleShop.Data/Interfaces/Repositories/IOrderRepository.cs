using System.Collections.Generic;
using SimpleShop.Data.Models;

namespace SimpleShop.Data.Interfaces.Repositories
{
	public interface IOrderRepository : IRepository<Order>
	{
		IEnumerable<Order> GetOrdersByUserId(string userId);
	}
}
