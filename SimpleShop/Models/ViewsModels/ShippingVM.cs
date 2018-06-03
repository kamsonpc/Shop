using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleShop.Models.ViewsModels
{
	public class ShippingVM
	{
		
		
		[Required]
		[Display(Name = "Imię i Nazwisko")]
		public string NameAndSurname { get; set; }
		[Required]
		[DataType(DataType.PhoneNumber)]
		[Display(Name = "Numer Telefonu")]
		public string PhoneNumber { get; set; }
		[Required]
		[Display(Name = "Adres")]
		public string Address { get; set; }
		[Display(Name = "Kod Pocztowy")]
		[Required]
		public string CityCode { get; set; }
		[Required]
		[Display(Name = "Kraj")]
		public string Country { get; set; }
	}
}