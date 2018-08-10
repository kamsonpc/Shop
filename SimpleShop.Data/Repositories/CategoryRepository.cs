using SimpleShop.Data.Interfaces.Repositories;
using SimpleShop.Data.Models;

namespace SimpleShop.Data.Repositories
{

	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		public CategoryRepository(ApplicationDbContext context) : base(context)
		{
		}

		public bool Update(Category category)
		{
			var categoryInDb = Get(category.CategoryId);
			if (categoryInDb == null) return false;
			categoryInDb.Name = category.Name;
			return true;

		}

		public ApplicationDbContext ApplicationDbContext
		{
			get { return Contex as ApplicationDbContext; }
		}

	}
}