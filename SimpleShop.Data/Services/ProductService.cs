using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SimpleShop.Data.Interfaces;
using SimpleShop.Data.Interfaces.Services;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.SearchModels;

namespace SimpleShop.Data.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private const string UploadFolderPath = "~/UploadedFiles";

		public ProductService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public List<Product> GetAll()
		{
			return _unitOfWork.Products.GetAll().ToList();
		}

		public List<Product> GetByPrice(int min, int max)
		{
			return _unitOfWork.Products.GetByPrice(min,max).ToList();
		}

		public List<Product> GetByCategory(int id)
		{
			return _unitOfWork.Products.GetByCategory(id).ToList();
		}

		public Product GetById(int id)
		{
			return _unitOfWork.Products.Get(id);
		}

		public List<Product> Search(ProductSearchModel searchModel,int? categoryId)
		{
			return _unitOfWork.Products.Search(categoryId,searchModel).ToList();
		}


		public void AddNew(Product product)
		{
			product.AddDate = DateTime.Now;
			_unitOfWork.Products.Add(product);
			_unitOfWork.Complete();
		}

		public void Remove(int id)
		{
			var productToRemove = _unitOfWork.Products.Get(id);
			if (productToRemove == null) return;
			RemoveImage(productToRemove.Img);
			_unitOfWork.Products.Remove(productToRemove);
			_unitOfWork.Complete();
		}

		public string UploadImage(HttpPostedFileBase file)
		{
			var path = Path.Combine(HttpContext.Current.Server.MapPath(UploadFolderPath), Path.GetFileName(file.FileName) ?? throw new InvalidOperationException());
			file.SaveAs(path);

			return file.FileName;
		}

		public void RemoveImage(string fileName)
		{
			var path = Path.Combine(HttpContext.Current.Server.MapPath(UploadFolderPath), fileName);
			try
			{
				File.Delete(path);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw new IOException();
			}
		}

		public void Update(int id, Product product)
		{
			_unitOfWork.Products.Update(id, product);
			_unitOfWork.Complete();
		}
	}
}