using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Services
{
	public class ProductService : IProductService
	{
		private readonly IApplicationDbContex _applicationDb;
		public ProductService(IApplicationDbContex applicationDb)
		{
			_applicationDb = applicationDb;
		}

		public List<ProductVM> GetAll()
		{
			var products = _applicationDb.Products.OrderBy(m => m.AddDate).ToList();
			return Mapper.Map<List<Product>,List<ProductVM>>(products);
		}

		public List<ProductVM>GetByCategory(int id)
		{
			var products = _applicationDb.Products.Where(b => b.Category.CategoryId == id).ToList();
			return Mapper.Map<List<Product>, List<ProductVM>>(products);
		}

		public ProductVM GetById(int id)
		{
			var product = _applicationDb.Products.SingleOrDefault(p => p.ProductId == id);
			return Mapper.Map<Product,ProductVM>(product);
		}


		public void AddNew(ProductVM productVM)
		{
			var product = Mapper.Map<ProductVM, Product>(productVM);
			product.AddDate = DateTime.Now;
			_applicationDb.Products.Add(product);
			_applicationDb.SaveChanges();
		}

		public bool Update(int id, ProductVM productVm)
		{
			var product = Mapper.Map<ProductVM, Product>(productVm);
			var productInDb = _applicationDb.Products.SingleOrDefault(p => p.ProductId == id); 
			if (productInDb != null)
			{
				productInDb.Quantity = product.Quantity;
				productInDb.Description = product.Description;
				productInDb.Name = product.Name;
				productInDb.Price = product.Price;
				productInDb.CategoryId = product.CategoryId;
				_applicationDb.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
			
		}

		public void Remove(int id)
		{
			var productInDb = _applicationDb.Products.SingleOrDefault(p => p.ProductId == id);
			if (productInDb != null)
			{
				try
				{
					RemoveImage(productInDb.Img);
					_applicationDb.Products.Remove(productInDb);
					_applicationDb.SaveChanges();
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw new Exception("File not removed");
				}
			}	
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
			if (productInDb!=null)
			{
				productInDb.Quantity -= quantity;
				_applicationDb.SaveChanges();
			}
			
		}
	}
}