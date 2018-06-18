using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models;
using SimpleShop.Models.SearchModels;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Services
{
	public class ProductService : IProductService
	{
		private readonly UnitOfWork _unitOfWork;

		public ProductService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public List<ProductVM> GetAll()
		{
			return Mapper.Map<List<Product>,List<ProductVM>>(_unitOfWork.Products.GetAll().ToList());
		}

		public List<ProductVM> GetByPrice(int min, int max)
		{
			return Mapper.Map<List<Product>, List<ProductVM>>(_unitOfWork.Products.GetByPrice(min,max).ToList());
		}

		public List<ProductVM> GetByCategory(int id)
		{
			return Mapper.Map<List<Product>, List<ProductVM>>(_unitOfWork.Products.GetByCategory(id).ToList());
		}

		public ProductVM GetById(int id)
		{
			return Mapper.Map<Product,ProductVM>(_unitOfWork.Products.Get(id));
		}

		public List<ProductVM> Search(ProductSearchModel searchModel,int? categoryId)
		{
			return Mapper.Map<List<Product>, List<ProductVM>>(_unitOfWork.Products.Search(categoryId,searchModel).ToList());
		}


		public void AddNew(ProductVM product)
		{
			_unitOfWork.Products.Add(Mapper.Map<ProductVM,Product>(product));
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
			var path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName) ?? throw new InvalidOperationException());
			file.SaveAs(path);

			return file.FileName;
		}

		public void RemoveImage(string fileName)
		{
			var path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
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

		public void Update(int id, ProductVM productVm)
		{
			var product = Mapper.Map<ProductVM, Product>(productVm);
			_unitOfWork.Products.Update(id, product);
			_unitOfWork.Complete();
		}
	}
}