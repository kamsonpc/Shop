using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleShop.Models.ViewsModels
{
	public class ProductViewModel
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
		public float Price { get; set; }
		[Display(Name = "Opis")]
		[MinLength(156)]
		[Required]
		public string Description { get; set; }
		[Display(Name = "Obrazek")]
		public string Img { get; set; }
		[Required]
		[Display(Name = "Ilość Sztuk")]
		public int Quantity { get; set; }

		public IEnumerable<SelectListItem> Categories { get; set; }

		public int CategoryId { get; set; }
	}
}