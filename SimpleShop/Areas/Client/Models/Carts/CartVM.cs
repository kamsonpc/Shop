using System.ComponentModel.DataAnnotations;
using SimpleShop.Data.Models;

namespace SimpleShop.Areas.Client.Models.Carts
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
		public Data.Models.Product Product { get; set; }
		[Required]
		[Display( Name = "Zamówiona ilość Sztuk")]
		public int OrderedQuantity { get; set; }

	}
}