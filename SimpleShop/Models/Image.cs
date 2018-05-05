﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleShop.Models
{
	public class Image
	{
		[ForeignKey("Product")]
		[Key]

		public int ImageId { get; set; }
		public string ImgPath { get; set; }
		public virtual Product Product { get; set; }
		}
}