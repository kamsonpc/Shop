using SimpleShop.Data.Interfaces.Repositories;
using System;

namespace SimpleShop.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        ICartRepository CartItems { get; }
        IUserAddress UserAddress { get; }
        IFoldersRepository Folders { get; }
        IFilesRepository Files { get; }

        int Complete();
    }
}