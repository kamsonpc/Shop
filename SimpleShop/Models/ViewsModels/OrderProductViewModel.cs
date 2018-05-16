using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleShop.Models.ViewsModels
{
	public class OrderProductViewModel
	{
		[Key]
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public string ApplicationUserId { get; set; }

		public int Price { get; set; }
		public int Quantity { get; set; }

		public DateTime Date { get; set; }

	}
}