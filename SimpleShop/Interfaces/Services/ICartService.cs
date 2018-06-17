using System.Collections.Generic;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Interfaces.Services
{
	public interface ICartService
	{
		List<CartVM> GetAll(string userId);
		void Complete(string userId, ShippingVM shippingData);
		void Add(CartVM cartItem);
		void Remove(int id);
		int Counter(string userId);
	}
}