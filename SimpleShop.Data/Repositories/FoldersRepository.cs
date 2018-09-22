using SimpleShop.Data.Interfaces.Repositories;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.Folders;
using System.Data.Entity;

namespace SimpleShop.Data.Repositories
{
    public class FoldersRepository : Repository<Folder>, IFoldersRepository
    {

        public FoldersRepository(DbContext context) : base(context)
        {

        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Contex as ApplicationDbContext; }
        }

    }
}