using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SimpleShop.Models.ViewsModels
{
	public class ProductVM
	{
		[Key]
		public int ProductId { get; set; }

		[MaxLength(30)]
		[Display(Name = "Nazwa")]
		[Required]
		public string Name { get; set; }

		[Display(Name = "Cena")]
		[DataType(DataType.Currency)]
		[Required]
		[Range(1, 10000000)]
		public decimal Price { get; set; }

		[Display(Name = "Opis")]
		[MinLength(135)]
		[Required]
		public string Description { get; set; }

		[Display(Name = "Obrazek")]
		public string Img { get; set; }

		[Required]
		[Range(1,1000)]
		[Display(Name = "Ilość Sztuk")]
		public int Quantity { get; set; }

		[Display(Name = "Zamówiona Ilość Sztuk")]
		public int CustomerQuantity { get; set; }

		[Display(Name = "Kategoria")]
		public IEnumerable<SelectListItem> Categories { get; set; }

		[Display(Name = "Kategoria")]
		[Required]
		public int CategoryId { get; set; }
	}
}