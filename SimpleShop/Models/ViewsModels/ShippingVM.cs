using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Models.ViewsModels
{
	public class ShippingVM
	{
		[Required]
		[Display(Name = "Imię i Nazwisko")]
		[MaxLength(255)]
		[MinLength(5)]
		public string NameAndSurname { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		[Display(Name = "Numer Telefonu")]
		public string PhoneNumber { get; set; }

		[Required]
		[MaxLength(255)]
		[MinLength(5)]
		[Display(Name = "Adres")]
		public string Address { get; set; }

		[Display(Name = "Kod Pocztowy")]
		[Required]
		[DataType(DataType.PostalCode)]
		public string CityCode { get; set; }

		[Required]
		[Display(Name = "Kraj")]
		public string Country { get; set; }
	}
}