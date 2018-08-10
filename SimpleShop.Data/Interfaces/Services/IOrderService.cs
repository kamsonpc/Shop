using System.Collections.Generic;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.Orders;

namespace SimpleShop.Data.Interfaces.Services
{
	public interface IOrderService
	{
		List<OrderInfo> GetAll();
		List<OrderInfo> Find(string search);
		List<Order> GetByUserId(string id);
		Order GetShippinDataById(int id);
		void Pay(int orderId);
	}
}