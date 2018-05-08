//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web;
//using SimpleShop.Interfaces;
//using SimpleShop.Models;

//namespace SimpleShop.Services
//{
//	public class UploadService : IUploadService
//	{
//		private readonly ApplicationDbContext _applicationDb;
//		public UploadService(ApplicationDbContext applicationDb)
//		{
//			_applicationDb = applicationDb;
//		}

//		public void UploadImage(Image image)
//		{
//			_applicationDb.Images.Add(image);
//			_applicationDb.SaveChanges();
//		}

//		public bool RemoveImage(int id)
//		{
//			try
//			{
//				var image = GetImgById(id);
//				if (image != null)
//				{
//					File.Delete(image.ImgPath);
//					_applicationDb.Images.Remove(image);
//					_applicationDb.SaveChanges();
//				}
//				return true;
//			}
//			catch 
//			{
//				return false;
//			}
			
//		}
		
//		public Image GetImgById(int id)
//		{
//			return _applicationDb.Images.SingleOrDefault(i => i.ImageId == id);
//		}

//		public List<Image> GetImages()
//		{
//			return _applicationDb.Images.ToList();
//		}
//	}
//}