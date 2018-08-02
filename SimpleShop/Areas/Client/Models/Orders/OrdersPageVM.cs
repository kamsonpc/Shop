using System;
using System.ComponentModel.DataAnnotations;
using SimpleShop.Data.Models;

namespace SimpleShop.Areas.Client.Models.Orders
{
	public class OrdersPageVM
	{
		[Key]
		[Display(Name = "Numer Zamówienia")]
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public Data.Models.Product Product { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }

		[DataType(DataType.Currency)]
		[Display(Name = "Cena")]
		[Required]
		[Range(1, 10000000)]
		public decimal Price { get; set; }

		[Display(Name = "Ilość sztuk")]
		[Range(0,100000)]
		public int Quantity { get; set; }

		[Display(Name = "Płatność Zaksięgowana")]
		public bool Payment { get; set; }

		[Display(Name = "Data Zakupu")]
		[DataType(DataType.DateTime)]
		public DateTime Date { get; set; }

	}
}