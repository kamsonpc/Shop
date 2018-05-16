using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace SimpleShop.Models
{
	public class Order
	{
		[Key]
		public int OrderId { get; set; }
		public  Product Product { get; set; }
		public  int ProductId { get; set; }
		
		public ApplicationUser ApplicationUser { get;set;}
		public string ApplicationUserId { get;set;}

		public int Price { get; set; }
		public int Quantity { get; set; }

		public DateTime Date { get; set; }
	}
}