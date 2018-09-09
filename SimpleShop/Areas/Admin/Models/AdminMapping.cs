using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using SimpleShop.Areas.Admin.Models.Categories;
using SimpleShop.Areas.Admin.Models.Products;
using SimpleShop.Data.Extensions;
using SimpleShop.Data.Models;

namespace SimpleShop.Areas.Admin.Models
{
	public class AdminMapping : Profile
	{
		public AdminMapping()
		{
			CreateMap<Category,CategoryViewModel>().ReverseMap();
			CreateMap<Product, ProductVM>();
		}
	}
}