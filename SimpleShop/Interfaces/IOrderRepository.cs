using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Interfaces
{
	public interface IOrderRepository
	{
		bool AddNew(ProductVM choosedProduct,ShippingVM shippingData,string UserId);
		List<OrdersPageVM> GetOrdersByUser(string id);
		List<OrdersPageVM> GetAllOrders();
		OrderVM  GetById(int id);
		ShippingVM GetShippingById(int id);
		bool ChangePayment(int id);
		List<OrdersPageVM> SearchByName(string query);


	}
}
