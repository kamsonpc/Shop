using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleShop.Models
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }
		public string Name { get; set; }
		public float Price { get; set; }
		public string Description { get; set; }
	}
}