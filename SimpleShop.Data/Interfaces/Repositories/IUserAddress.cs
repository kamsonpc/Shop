using SimpleShop.Data.Models;

namespace SimpleShop.Data.Interfaces.Repositories
{
	public interface  IUserAddress : IRepository<UserAddress>
	{
		void Update(int id,UserAddress userAddress);
	}
}
