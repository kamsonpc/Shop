using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Data.Models
{
	public class Cart
	{
		[Key]
		public int CartItemId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public Product Product { get; set; }
		public string ApplicationUserId { get; set; }
		public int ProductId { get; set; }
		public int OrderedQuantity{ get; set; }
	}
}