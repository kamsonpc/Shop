using SimpleShop.Data.Interfaces.Repositories;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.Files;
using System.Data.Entity;

namespace SimpleShop.Data.Repositories
{
    public class FilesRepository : Repository<File>, IFilesRepository
    {

        public FilesRepository(DbContext context) : base(context)
        {

        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Contex as ApplicationDbContext; }
        }

    }
}