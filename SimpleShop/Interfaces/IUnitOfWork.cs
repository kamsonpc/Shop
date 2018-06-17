using System;
using SimpleShop.Interfaces.Repositories;

namespace SimpleShop.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		ICategoryRepository Categories { get; }
		IOrderRepository Orders { get; }
		IProductRepository Products { get; }
		ICartRepository CartItems { get; }

		int Complete();
	}
}