using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Models.SearchModels
{
	public class ProductSearchModel
	{
		public int? PriceFrom { get; set; }
		public int? PriceTo { get; set; }
		public string Name { get; set; }

	}
}