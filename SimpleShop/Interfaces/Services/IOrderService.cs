using SimpleShop.Models.ViewsModels;
using System.Collections.Generic;

namespace SimpleShop.Interfaces.Services
{
	public interface IOrderService
	{
		List<OrderVM> GetAll();
		List<OrderVM> GetByUserId(string id);
		ShippingVM GetShippinDataById(int id);
		void Complete(int choosedProductId, string userId, ShippingVM shippingData);
	}
}