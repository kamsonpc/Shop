using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SimpleShop.Interfaces;
using SimpleShop.Models;

namespace SimpleShop.Services
{
	public class ProductService : IProductService
	{
		private readonly ApplicationDbContext _applicationDb;
		public ProductService(ApplicationDbContext applicationDb)
		{
			_applicationDb = applicationDb;
		}

		public List<Product> GetAll()
		{
			return _applicationDb.Products.OrderBy(m => m.AddDate).ToList();
		}

		public List<Product>GetByCategory(int id)
		{
			return _applicationDb.Products.Where(b => b.Category.CategoryId == id).ToList();
		}

		public Product GetById(int id)
		{
			return _applicationDb.Products.SingleOrDefault(p => p.ProductId == id);
		}


		public void AddNew(Product product)
		{
			product.AddDate = DateTime.Now;
			_applicationDb.Products.Add(product);
			_applicationDb.SaveChanges();
		}

		public bool Update(int id, Product product)
		{
			var productInDb = GetById(id);
			productInDb.Quantity = product.Quantity;
			productInDb.Description = product.Description;
			productInDb.Name = product.Name;
			productInDb.Price = product.Price;
			productInDb.CategoryId = product.CategoryId;
			_applicationDb.SaveChanges();
			return true;
		}

		public void Remove(int id)
		{
			var productInDb = GetById(id);
			RemoveImage(productInDb.Img);
			_applicationDb.Products.Remove(productInDb);
			_applicationDb.SaveChanges();
		}

		public string UploadImage(HttpPostedFileBase file)
		{
			string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName) ?? throw new InvalidOperationException());
			file.SaveAs(path);

			return file.FileName;
		}

		public void RemoveImage(string fileName)
		{
			string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
			File.Delete(path);
		}

		public void ChangeQuantity(int id, int quantity)
		{
			var productInDb = GetById(id);
			productInDb.Quantity -= quantity;
			_applicationDb.SaveChanges();
		}
	}
}