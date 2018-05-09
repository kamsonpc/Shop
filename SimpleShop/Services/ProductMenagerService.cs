using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using SimpleShop.Interfaces;
using SimpleShop.Models;

namespace SimpleShop.Services
{
	public class ProductMenagerService : IProductMenagerService
	{
		private readonly ApplicationDbContext _applicationDb;
		public ProductMenagerService(ApplicationDbContext applicationDb)
		{
			_applicationDb = applicationDb;
		}

		public List<Product> GetAll()
		{
			return	_applicationDb.Products.ToList();
		}

		public Product GetById(int id)
		{
			return _applicationDb.Products.SingleOrDefault(p => p.ProductId == id);
		}


		public void AddNew(Product product)
		{
			_applicationDb.Products.Add(product);
			_applicationDb.SaveChanges();
		}

		public bool Update(int id, Product product)
		{
			var productInDb = GetById(id);
			Mapper.Map<Product>(product);
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
			string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
			file.SaveAs(path);

			return file.FileName;
		}

		public void RemoveImage(string fileName)
		{
			string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
			File.Delete(path);
		}
	}
}