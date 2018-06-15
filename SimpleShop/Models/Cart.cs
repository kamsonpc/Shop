using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleShop.Models
{
	public class Cart
	{
		[Key]
		public int CartItemId { get; set; }
		[Required]
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public Product Product { get; set; }
		[Required]
		public int ProductId { get; set; }
		[Required]
		public int OrderedQuantity{ get; set; }
	}
}