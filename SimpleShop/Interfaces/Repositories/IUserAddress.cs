using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShop.Models;

namespace SimpleShop.Interfaces.Repositories
{
	public interface  IUserAddress : IRepository<UserAddress>
	{
		void Update(int id,UserAddress userAddress);
	}
}
