using System;
using SimpleShop.Interfaces.Repositories;

namespace SimpleShop.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		ICategoryRepository Categories { get; }
		IOrderRepository Orders { get; }
		int Complete();
	}
}