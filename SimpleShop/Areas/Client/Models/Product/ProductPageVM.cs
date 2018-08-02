using System.Collections.Generic;
using PagedList;
using SimpleShop.Areas.Admin.Models.Products;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.SearchModels;

namespace SimpleShop.Areas.Client.Models.Product
{
	public class ProductPageVM
	{
		public IPagedList<ProductVM> Product { get; set; }
		public List<Category> Categories  { get; set; }
		public ProductSearchModel Search { get; set; }
	}
}