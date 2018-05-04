using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SimpleShop.Interfaces;
using SimpleShop.Models;

namespace SimpleShop.Services
{
	public class UploadService : IUploadService
	{
		private readonly ApplicationDbContext _applicationDb;
		public UploadService(ApplicationDbContext applicationDb)
		{
			_applicationDb = applicationDb;
		}

		public void UploadImage(Image image)
		{
			_applicationDb.Images.Add(image);
			_applicationDb.SaveChanges();
		}

		public bool RemoveImage(int id)
		{
			try
			{
				var image = _applicationDb.Images.SingleOrDefault(i => i.ImageId == id);
				if (image != null)
				{
					File.Delete(image.ImgPath);
					_applicationDb.Images.Remove(image);
					_applicationDb.SaveChanges();
				}
				return true;
			}
			catch 
			{
				return false;
			}
			
		}
	}
}