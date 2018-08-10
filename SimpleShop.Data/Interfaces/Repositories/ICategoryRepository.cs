using SimpleShop.Data.Models;

namespace SimpleShop.Data.Interfaces.Repositories
{
	public interface ICategoryRepository : IRepository<Category>
	{
		bool Update(Category category);
	}
}
