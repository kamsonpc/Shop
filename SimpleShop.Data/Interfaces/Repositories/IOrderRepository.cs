using System.Collections.Generic;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.Orders;

namespace SimpleShop.Data.Interfaces.Repositories
{
	public interface IOrderRepository : IRepository<Order>
	{
		IEnumerable<OrderInfo> GetOrdersByUserId(string userId);
	}
}
