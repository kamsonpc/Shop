using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
		[MaxLength(512)]
		[Required]
		public string Description { get; set; }
		[Display(Name = "Obrazek")]
		public string Img { get; set; }
	}
}