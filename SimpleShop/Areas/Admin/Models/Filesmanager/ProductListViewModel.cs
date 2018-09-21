using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SimpleShop.Areas.Admin.Models.Filesmanager
{
	public class FolderViewModel
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
	}
}