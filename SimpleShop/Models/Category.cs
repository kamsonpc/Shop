﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace SimpleShop.Models
{
	public class Category
	{ 
		[Key]
		public int CategoryId { get; set; }
		public ICollection<Product> Products { get; set; }
		public string Name { get; set; }
	}
}