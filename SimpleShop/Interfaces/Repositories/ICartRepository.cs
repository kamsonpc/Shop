using SimpleShop.Models;

namespace SimpleShop.Interfaces.Repositories
{
	public interface ICartRepository : IRepository<Cart>
	{
		int Counter(string userId);
	}
}