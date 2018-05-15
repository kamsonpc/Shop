using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleShop.Models;

namespace SimpleShop.Interfaces
{
	public interface IProductService
	{
		List<Product> GetAll();
		Product GetById(int id);
		void AddNew(Product product,int quantity);
		bool Update(int id, Product product);
		void Remove(int id);
		string UploadImage(HttpPostedFileBase file);

		int CountProductUnits(int id);
	}
}