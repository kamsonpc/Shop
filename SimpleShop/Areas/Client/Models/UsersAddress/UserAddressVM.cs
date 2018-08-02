using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Areas.Client.Models.UsersAddress
{
	public class UserAddressVM
	{
		public int Id { get; set; }
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

		[Required]
		public string ApplicationUserId { get; set; }

	}
}