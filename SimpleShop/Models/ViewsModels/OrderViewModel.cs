using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleShop.Models.ViewsModels
{
	public class OrderViewModel
	{
		[Key]
		public int OrderId { get; set; }
		[Display(Name = "Nazwa")]
		public int ProductId { get; set; }
		public string ApplicationUserId { get; set; }
		[DataType(DataType.Currency)]
		[Display(Name = "Cena")]
		public float Price { get; set; }
		[Display(Name = "Ilość sztuk")]
		public int Quantity { get; set; }

		[Display(Name = "Data Zakupu")]
		public DateTime Date { get; set; }
		[Display(Name = "Płatność")]
		public bool Payment { get; set; }
	}
}