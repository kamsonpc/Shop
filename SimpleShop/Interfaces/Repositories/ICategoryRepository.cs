using System.Collections.Generic;
using System.Web.Mvc;
using SimpleShop.Models;

namespace SimpleShop.Interfaces.Repositories
{
	public interface ICategoryRepository : IRepository<Category>
	{
		List<SelectListItem> GetSelectList();
	}
}
