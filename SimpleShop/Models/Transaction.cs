using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Models
{
	public class Transaction
	{
		public int TransactionId { get; set; }

		public Product Product { get; set; }
		public int ProductId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }

		public bool Payment { get; set; }
	}
}