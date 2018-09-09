using SimpleShop.Data.Models.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Data.Models
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }

		public string Name { get; set; }

		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		public string Description { get; set; }

		public string Img { get; set; }

		public int Quantity { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime AddDate { get; set; }
		public virtual Category Category { get; set; }

		public int CategoryId { get; set; }
		public virtual ICollection<Order> Orders { get; set; }	

		public virtual ICollection<Cart> Cart { get; set; }
	}
}