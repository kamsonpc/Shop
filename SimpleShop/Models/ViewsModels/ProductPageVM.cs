using PagedList;
using SimpleShop.Models.SearchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleShop.Models.ViewsModels
{
	public class ProductPageVM
	{
		public IPagedList<ProductVM> Product { get; set; }
		public IEnumerable<Category> Categories  { get; set; }
		public ProductSearchModel Search { get; set; }
	}
}