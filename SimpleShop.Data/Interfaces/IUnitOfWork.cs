using System;
using SimpleShop.Data.Interfaces.Repositories;

namespace SimpleShop.Data.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		ICategoryRepository Categories { get; }
		IOrderRepository Orders { get; }
		IProductRepository Products { get; }
		ICartRepository CartItems { get; }
		IUserAddress UserAddress { get; }

		int Complete();
	}
}