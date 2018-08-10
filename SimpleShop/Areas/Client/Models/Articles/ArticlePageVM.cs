using System.Collections.Generic;
using PagedList;
using SimpleShop.Areas.Admin.Models.Products;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.SearchModels;

namespace SimpleShop.Areas.Client.Models.Articles
{
	public class ArticlePageVM
	{
		public IPagedList<ArticleViewModel> Product { get; set; }
		public List<Category> Categories  { get; set; }
		public ProductSearchModel Search { get; set; }
	}
}