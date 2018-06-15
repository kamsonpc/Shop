using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SimpleShop.Interfaces.Repositories;
using SimpleShop.Models;

namespace SimpleShop.Repositories
{

	public class CategoryRepository : Repository<Category>,ICategoryRepository
	{
		public CategoryRepository(ApplicationDbContext context) : base (context)
		{
		}

		public List<SelectListItem> GetSelectList()
		{
			return GetAll().Select(x => new SelectListItem
			{
				Value = x.CategoryId.ToString(),
				Text = x.Name
			}).ToList();
		}

		public ApplicationDbContext ApplicationDbContext		{
			get { return Contex as ApplicationDbContext; }
		}
		
	}
}