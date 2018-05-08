using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SimpleShop.Models;

namespace SimpleShop.Interfaces
{
	public interface IUploadService
	{
		void UploadImage(Image image);
		bool RemoveImage(int id);
		Image GetImgById(int id);
		List<Image> GetImages();
	}
}
