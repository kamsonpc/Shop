﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Models
{
	public class Order
	{
		[Key]
		public int OrderId { get; set; }

		public Product Product { get; set; }

		[Required]
		public int ProductId { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		[DataType(DataType.Currency)]
		[Required]
		[Range(1, 10000000)]
		public decimal Price { get; set; }

		[Required]
		[Range(1, 100000)]
		public int Quantity { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public bool Payment { get; set; }

		[Required]
		[MaxLength(255)]
		[MinLength(5)]
		public string NameAndSurname { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		[Required]
		[MaxLength(255)]
		[MinLength(5)]
		public string Address { get; set; }

		[Required]
		[DataType(DataType.PostalCode)]
		public string CityCode { get; set; }

		[Required]
		public string Country { get; set; }

	}
}