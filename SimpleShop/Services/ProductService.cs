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
		public List<ProductVM> GetAll()
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var products = ctx.Products.OrderByDescending(m => m.AddDate).ToList();
				return Mapper.Map<List<Product>, List<ProductVM>>(products);
			}
		}

		public List<ProductVM> GetByPrice(int minPrice,int maxPrice)
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var products = ctx.Products.OrderByDescending(m => m.AddDate).Where(p => p.Price <= maxPrice).Where(p => p.Price >= minPrice).ToList();
				return Mapper.Map<List<Product>, List<ProductVM>>(products);
			}
		}

		public List<ProductVM> GetByCategory(int id)
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var products = ctx.Products.Where(b => b.Category.CategoryId == id).ToList();
				return Mapper.Map<List<Product>, List<ProductVM>>(products);
			}
		}

		public ProductVM GetById(int id)
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var product = ctx.Products.SingleOrDefault(p => p.ProductId == id);
				return Mapper.Map<Product, ProductVM>(product);
			}
		}

		public void AddNew(ProductVM productVM)
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var product = Mapper.Map<ProductVM, Product>(productVM);
				product.AddDate = DateTime.Now;
				ctx.Products.Add(product);
				ctx.SaveChanges();
			}
		}

		public void Update(int id, ProductVM productVm)
		{
			var product = Mapper.Map<ProductVM, Product>(productVm);
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var productInDb = ctx.Products.SingleOrDefault(p => p.ProductId == id);
				if (productInDb != null)
				{
					productInDb.Quantity = product.Quantity;
					productInDb.Description = product.Description;
					productInDb.Name = product.Name;
					productInDb.Price = product.Price;
					productInDb.CategoryId = product.CategoryId;
					ctx.SaveChanges();
				}
			}
		}

		public void Remove(int id)
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var productInDb = ctx.Products.SingleOrDefault(p => p.ProductId == id);
				if (productInDb != null)
				{
					RemoveImage(productInDb.Img);
					ctx.Products.Remove(productInDb);
					ctx.SaveChanges();
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
			try
			{
				File.Delete(path);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw new Exception("File not removed");
			}
		}

		public void ChangeQuantity(int id, int quantity)
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var productInDb = GetById(id);
				if (productInDb != null)
				{
					productInDb.Quantity -= quantity;
					ctx.SaveChanges();
				}
			}
		}

		public int Quantity(int id)
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var product = ctx.Products.SingleOrDefault(p => p.ProductId == id);
				return product.Quantity;
			}
		}
	}
}