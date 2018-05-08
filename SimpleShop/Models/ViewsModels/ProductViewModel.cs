using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleShop.Models.ViewsModels
{
	public class ProductViewModel
	{
		[Key]
		public int ProductId { get; set; }
		[MaxLength(30)]
		public string Name { get; set; }
		[DataType(DataType.Currency)]
		public float Price { get; set; }
		[MaxLength(255)]
		public string Description { get; set; }
		public string Img { get; set; }
	}
}