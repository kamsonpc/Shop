using System.Collections.Generic;
using SimpleShop.Data.Models;

namespace SimpleShop.Data.Interfaces.Repositories
{
	public interface ICartRepository : IRepository<Cart>
	{
		IEnumerable<Cart> GetAll(string userId);
		int Counter(string userId);
	}
}