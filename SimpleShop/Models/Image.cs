using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleShop.Models
{
	public class Image
	{
		[Key]
		public int ImageId { get; set; }
		public string ImgPath { get; set; }
	}
}