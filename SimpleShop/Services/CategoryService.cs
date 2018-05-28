using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SimpleShop.Interfaces;
using SimpleShop.Models;

namespace SimpleShop.Services
{

	public class CategoryService : ICategoryService
	{

		public List<Category> GetAll()
		{
			using (var ctx = new ApplicationDbContext())
			{
				return  ctx.Categories.ToList();
			}
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
			using (var ctx = new ApplicationDbContext())
			{
				return ctx.Categories.SingleOrDefault(c => c.CategoryId == id);
			}
		}

		public void AddNew(Category category)
		{
			using (var ctx = new ApplicationDbContext())
			{
				ctx.Categories.Add(category);
				ctx.SaveChanges();
			}
		}

		public void Update(int id, Category category)
		{
			using (var ctx = new ApplicationDbContext())
			{
				var categoryInDb = GetById(id);
				categoryInDb.Name = category.Name;
				ctx.SaveChanges();
			}
		}

		public void Remove(int id)
		{
			var categoryInDb = GetById(id);
			if (categoryInDb != null)
			{
				using (var ctx = new ApplicationDbContext())
				{
					ctx.Categories.Remove(categoryInDb);
				}
			}
		}
	}
}