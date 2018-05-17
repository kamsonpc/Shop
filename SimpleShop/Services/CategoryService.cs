using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Interfaces;
using SimpleShop.Models;

namespace SimpleShop.Services
{

	public class CategoryService : ICategoryService
	{
		private readonly ApplicationDbContext _applicationDb;
		public CategoryService(ApplicationDbContext applicationDb)
		{
			_applicationDb = applicationDb;
		}

		public List<Category> GetAll()
		{
			return _applicationDb.Categories.ToList();
		}

		public IEnumerable<SelectListItem> GetSelectList()
		{
			return GetAll().Select(x => new SelectListItem
			{
				Value = x.CategoryId.ToString(),
				Text = x.Name
			});
		}

		public Category GetById(int id)
		{
			return _applicationDb.Categories.SingleOrDefault(c => c.CategoryId == id);
		}

		public void AddNew(Category category)
		{
			_applicationDb.Categories.Add(category);
			_applicationDb.SaveChanges();
		}

		public bool Update(int id, Category category)
		{
			var categoryInDb = GetById(id);
			categoryInDb.Name = category.Name;
			_applicationDb.SaveChanges();
			return true;
		}

		public void Remove(int id)
		{
			var categoryInDb = GetById(id);
			if (categoryInDb != null)
			{
				_applicationDb.Categories.Remove(categoryInDb);
			}
		}
	}
}