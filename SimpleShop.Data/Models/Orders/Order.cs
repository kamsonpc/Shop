using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Data.Models.Orders
{
	public class Order
	{
		[Key]
		public int OrderId { get; set; }

		public virtual Product Product { get; set; }

		public int ProductId { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }

		public string ApplicationUserId { get; set; }

		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		public int Quantity { get; set; }

		public DateTime Date { get; set; }

		public bool Payment { get; set; }

		public string NameAndSurname { get; set; }

		public string PhoneNumber { get; set; }

		public string Address { get; set; }

		public string CityCode { get; set; }

		public string Country { get; set; }

	}
}