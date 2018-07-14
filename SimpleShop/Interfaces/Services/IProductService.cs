using System.Collections.Generic;
using System.Web;
using SimpleShop.Models;
using SimpleShop.Models.SearchModels;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Interfaces.Services
{
	public interface IProductService
	{
		List<Product> GetAll();
		List<Product> Search(ProductSearchModel searchModel, int? categoryId);
		Product GetById(int id);
		void AddNew(Product product);
		void Update(int id, Product product);

		void Remove(int id);

		string UploadImage(HttpPostedFileBase file);
		void RemoveImage(string fileName);
	}
}