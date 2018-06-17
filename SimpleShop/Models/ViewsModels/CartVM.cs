using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Models.ViewsModels
{
	public class CartVM
	{
		[Key]
		public int CartItemId { get; set; }
		public Product Product { get; set; }
		[Required]
		public string ApplicationUserId { get; set; }
		[Required]
		public int ProductId { get; set; }
		[Required]
		public int OrderedQuantity { get; set; }
	}
}