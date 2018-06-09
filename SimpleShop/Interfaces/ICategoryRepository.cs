using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SimpleShop.Models;

namespace SimpleShop.Interfaces
{
	public interface ICategoryRepository
	{
		List<Category> GetAll();
		Category GetById(int id);
		void AddNew(Category category);
		void Update(int id, Category category);
		void Remove(int id);
		IEnumerable<SelectListItem> GetSelectList();
	}
}
