using System;
using System.ComponentModel.DataAnnotations;
using SimpleShop.Data.Models;

namespace SimpleShop.Areas.Client.Models.Orders
{
	public class OrdersPageVM
	{
		[Display(Name = "Numer Zamówienia")]
		public int Id { get; set; }

		[Display(Name = "Product")]
		public string ProductName { get; set; }

		[DataType(DataType.Currency)]
		[Display(Name = "Cena")]
		[Required]
		[Range(1, 10000000)]
		public decimal Price { get; set; }

		[Display(Name = "Ilość sztuk")]
		[Range(0, 100000)]
		public int Quantity { get; set; }

		[Display(Name = "Data Zakupu")]
		[DataType(DataType.DateTime)]
		public DateTime Date { get; set; }

		[Display(Name = "Płatność Zaksięgowana")]
		public bool Payment { get; set; }

	}
}