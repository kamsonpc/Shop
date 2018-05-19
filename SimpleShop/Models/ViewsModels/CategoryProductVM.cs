using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleShop.Models.ViewsModels
{
	public class CategoryProductVM
	{
		public List<ProductVM> product { get; set; }
		public ICollection<Category> categories  { get; set; }
	}
}