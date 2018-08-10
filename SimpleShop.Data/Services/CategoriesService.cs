using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleShop.Data.Interfaces;
using SimpleShop.Data.Interfaces.Services;
using SimpleShop.Data.Models;

namespace SimpleShop.Data.Services
{
	public class CategoriesService : ICategoriesService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoriesService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public List<Category> GetAll()
		{
			return _unitOfWork.Categories.GetAll().OrderBy(m => m.CategoryId).ToList();
		}

		public void Update(Category category)
		{
			_unitOfWork.Categories.Update(category);
			_unitOfWork.Complete();
		}

		public void Add(Category category)
		{
			_unitOfWork.Categories.Add(category);
			_unitOfWork.Complete();
		}

		public Category GetById(int id)
		{
			return _unitOfWork.Categories.Get(id);
		}

		public void Remove(int id)
		{
			var categoryToRemove = GetById(id); 
			_unitOfWork.Categories.Remove(categoryToRemove);
			_unitOfWork.Complete();
		}
	}
}