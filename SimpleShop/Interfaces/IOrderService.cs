using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Interfaces
{
	public interface IOrderService
	{
		void AddNew(Order order);
		List<OrderProductUserVM> GetOrdersByUser(string id);
		List<OrderProductUserVM> GetAllOrders();
		OrderVM  GetById(int id);
		ShippingVM GetShippingById(int id);
		bool ChangePayment(int id);
		List<OrderProductUserVM> SearchByName(string query);


	}
}
