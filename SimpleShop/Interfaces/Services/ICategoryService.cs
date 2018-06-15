using System.Collections.Generic;
using System.Web.Mvc;
using SimpleShop.Models;

namespace SimpleShop.Interfaces.Services
{
	public interface ICategoryService
	{
		List<SelectListItem> GetSelectList();

		List<Category> GetAll();
		Category GetById(int id);

		void AddNew(Category category);

		void Remove(int id);
		void RemoveRange(List<Category> categories);
	}
}