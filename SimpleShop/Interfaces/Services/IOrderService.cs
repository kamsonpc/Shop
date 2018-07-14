using SimpleShop.Models.ViewsModels;
using System.Collections.Generic;
using SimpleShop.Models;

namespace SimpleShop.Interfaces.Services
{
	public interface IOrderService
	{
		List<Order> GetAll();
		List<Order> Find(string search);
		List<Order> GetByUserId(string id);
		Order GetShippinDataById(int id);
		void Pay(int orderId);
	}
}