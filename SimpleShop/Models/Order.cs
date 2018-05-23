using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace SimpleShop.Models
{
	public class Order
	{
		[Key]
		public int OrderId { get; set; }
	
		public  Product Product { get; set; }

		[Required]
		public int ProductId { get; set; }

		public virtual ApplicationUser ApplicationUser { get;set;}

		[Required]
		public string ApplicationUserId { get;set;}

		[DataType(DataType.Currency)]
		[Required]
		public float Price { get; set; }

		[Required]
		public int Quantity { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public bool Payment { get; set; }
		[Required]
		public string NameAndSurname { get; set; }

		[Required]
		public string PhoneNumber { get; set; }

		[Required]
		public string Address { get; set; }
		[Required]
		public string CityCode { get; set; }
		[Required]
		public string Country { get; set; }
		
	}
}