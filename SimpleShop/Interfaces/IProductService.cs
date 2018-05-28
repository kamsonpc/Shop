using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Interfaces
{
	public interface IProductService
	{
		List<ProductVM> GetAll();
		List<ProductVM> GetByCategory(int id);
		ProductVM GetById(int id);
		void AddNew(ProductVM product);
		void Update(int id, ProductVM product);
		void Remove(int id);
		string UploadImage(HttpPostedFileBase file);
		void ChangeQuantity(int id,int quantity);
	}
}