using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleShop.Models.ViewsModels
{
	public class OrderVM
	{
		[Key]
		public int OrderId { get; set; }
	
		[Display(Name = "Nazwa")]
		public int ProductId { get; set; }

		public string ApplicationUserId { get; set; }

		[DataType(DataType.Currency)]
		[Display(Name = "Cena")]
		[Range(1, 10000000)]
		public decimal Price { get; set; }

		[Display(Name = "Ilość sztuk")]
		[Range(1, 100000)]
		public int Quantity { get; set; }

		[Display(Name = "Data Zakupu")]
		[DataType(DataType.DateTime)]
		public DateTime Date { get; set; }

		[Display(Name = "Płatność Zaksięgowana")]
		public bool Payment { get; set; }

		
	}
}