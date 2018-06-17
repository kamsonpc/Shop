using SimpleShop.Models.ViewsModels;
using System.Collections.Generic;

namespace SimpleShop.Interfaces.Services
{
	public interface IOrderService
	{
		List<OrdersPageVM> GetAll();
		List<OrdersPageVM> Find(string search);
		List<OrdersPageVM> GetByUserId(string id);
		ShippingVM GetShippinDataById(int id);
		void Pay(int orderId);
	}
}