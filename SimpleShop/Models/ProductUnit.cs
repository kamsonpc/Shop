using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Models
{
	public class ProductUnit
	{
		public int ProductUnitId { get; set; }
		public Product Product { get; set; }
		public int ProductId { get; set; }

	}
}