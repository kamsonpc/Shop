using System.Data.Entity;
using System.Linq;
using SimpleShop.Data.Interfaces.Repositories;
using SimpleShop.Data.Models;

namespace SimpleShop.Data.Repositories
{
	public class UserAddressRepository : Repository<UserAddress>,IUserAddress
	{
		public UserAddressRepository(DbContext context) : base(context)
		{
		}

		public void Update(int id, UserAddress userAddress)
		{
			var userAddressInDb = Find(ua => ua.Id == id).SingleOrDefault();
			if(userAddressInDb == null) return;
			userAddressInDb.Address = userAddress.Address;
			userAddressInDb.CityCode = userAddress.CityCode;
			userAddressInDb.PhoneNumber = userAddress.PhoneNumber;
			userAddressInDb.NameAndSurname = userAddress.NameAndSurname;
			userAddressInDb.Country = userAddress.Country;
		}
	
		public ApplicationDbContext ApplicationDbContext
		{
			get { return Contex as ApplicationDbContext; }
		}


	}

}