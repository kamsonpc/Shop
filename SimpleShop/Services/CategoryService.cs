using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models;

namespace SimpleShop.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly UnitOfWork _unitOfWork;

		public CategoryService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public List<Category> GetAll()
		{
			return _unitOfWork.Categories.GetAll().ToList();
		}

		public Category GetById(int id)
		{
			return _unitOfWork.Categories.Find(c => c.CategoryId == id).SingleOrDefault();
		}

		public void AddNew(Category category)
		{
			_unitOfWork.Categories.Add(category);
			_unitOfWork.Complete();
		}

		public void Update(int id,Category category)
		{
			var categoryInDb = _unitOfWork.Categories.Get(id);

			if (categoryInDb != null)
			{
				categoryInDb.Name = category.Name;
				_unitOfWork.Complete();
			}

		}

		public void Remove(int id)
		{
			var categoryToRemove = GetById(id);
			_unitOfWork.Categories.Remove(categoryToRemove);
			_unitOfWork.Complete();

		}

		public void RemoveRange(List<Category> categories)
		{
			_unitOfWork.Categories.RemoveRange(categories);
			_unitOfWork.Complete();
		}

		public List<SelectListItem> GetSelectList()
		{
			return _unitOfWork.Categories.GetSelectList();
		}
	}
}