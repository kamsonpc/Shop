using System.Collections.Generic;
using SimpleShop.Data.Models;

namespace SimpleShop.Data.Interfaces.Services
{
	public interface ICategoriesService
	{
		List<Category> GetAll();
		void Update(Category category);
		void Add(Category category);
		Category GetById(int Id);
		void Remove(int id);
	}
}