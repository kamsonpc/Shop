using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Models.SearchModels
{
	public class ProductSH
	{
		public int? CategoryId { get; set; }
		public int? PriceFrom { get; set; }
		public int? PriceTo { get; set; }
		public string Name { get; set; }

	}
}