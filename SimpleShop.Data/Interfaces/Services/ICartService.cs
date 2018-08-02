using System.Collections.Generic;
using SimpleShop.Data.Models;

namespace SimpleShop.Data.Interfaces.Services
{
	public interface ICartService
	{
		List<Cart> GetAll(string userId);
		//int Complete(string userId, ShippingVM shippingData);
		void Add(Cart cartItem);
		void Remove(int id);
		int Counter(string userId);
	}
}