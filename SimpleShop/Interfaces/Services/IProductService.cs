using System.Collections.Generic;
using System.Web;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Interfaces.Services
{
	public interface IProductService
	{
		List<ProductVM> GetAll();
		List<ProductVM> GetByPrice(int min, int max);
		List<ProductVM> GetByCategory(int id);

		ProductVM GetById(int id);
		void AddNew(ProductVM product);
		void Update(int id, ProductVM product);

		void Remove(int id);

		string UploadImage(HttpPostedFileBase file);
		void RemoveImage(string fileName);
	}
}