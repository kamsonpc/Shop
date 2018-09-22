using SimpleShop.Data.Interfaces;
using SimpleShop.Data.Interfaces.Repositories;
using SimpleShop.Data.Repositories;
using System;

namespace SimpleShop.Data.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _contex;
        public UnitOfWork(ApplicationDbContext contex)
        {
            _contex = contex;
            Categories = new CategoryRepository(_contex);
            Orders = new OrderRepository(_contex);
            Products = new ProductRepository(_contex);
            CartItems = new CartRepository(_contex);
            UserAddress = new UserAddressRepository(_contex);
            Folders = new FoldersRepository(_contex);

        }

        public ICategoryRepository Categories { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IProductRepository Products { get; private set; }
        public ICartRepository CartItems { get; private set; }
        public IUserAddress UserAddress { get; private set; }
        public IFoldersRepository Folders { get; private set; }
        public IFilesRepository Files { get; private set; }

        public int Complete()
        {
            return _contex.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}