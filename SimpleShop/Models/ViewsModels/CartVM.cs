using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Models.ViewsModels
{
	public class CartVM
	{
		[Key]
		public int CartItemId { get; set; }
		[Required]
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		[Display(Name = "Nazwa")]
		public int ProductId { get; set; }
		public Product Product { get; set; }
		[Required]
		[Display( Name = "Zamówiona ilość Sztuk")]
		public int OrderedQuantity { get; set; }

	}
}