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
		[Required]
		public int Quantity { get; set; }
		[Required]
		public DateTime AddDate { get; set; }
		public virtual Category Category { get; set; }
		[Required]
		public int CategoryId { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
		
	}
}