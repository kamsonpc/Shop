using System.Collections.Generic;
using SimpleShop.Models;

namespace SimpleShop.Interfaces.Repositories
{
	public interface ICartRepository : IRepository<Cart>
	{
		IEnumerable<Cart> GetAll(string userId);
		int Counter(string userId);
	}
}