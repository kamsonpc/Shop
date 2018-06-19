using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Models
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }

		[Required]
		public string Name { get; set; }

		[DataType(DataType.Currency)]
		[Required]
		[Range(1, 10000000)]
		public decimal Price { get; set; }

		[Required]
		[MinLength(135)]
		public string Description { get; set; }

		public string Img { get; set; }

		[Required]
		[Range(0, 100000)]
		public int Quantity { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public DateTime AddDate { get; set; }
		public virtual Category Category { get; set; }

		[Required]
		public int CategoryId { get; set; }
		public virtual ICollection<Order> Orders { get; set; }	

		public virtual ICollection<Cart> Cart { get; set; }
	}
}