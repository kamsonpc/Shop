using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleShop.Models
{
	public class Product
	{

		[Key]
		public int ProductId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public float Price { get; set; }
		[Required]
		public string Description { get; set; }
		public string Img { get; set; }
		public int Quantity { get; set; }

		public Category Category { get; set; }
		public int CategoryId { get; set; }
		public ICollection<Order> Orders { get; set; }
		
	}
}