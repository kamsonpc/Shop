using PagedList;
using SimpleShop.Models.SearchModels;
using System.Collections.Generic;

namespace SimpleShop.Models.ViewsModels
{
	public class ProductPageVM
	{
		public IPagedList<ProductVM> Product { get; set; }
		public List<Category> Categories  { get; set; }
		public ProductSearchModel Search { get; set; }
	}
}