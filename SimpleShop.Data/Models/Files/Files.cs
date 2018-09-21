using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleShop.Data.Models.Behaviours;

namespace SimpleShop.Data.Models.Files
{
	public class Files : IEntity,IAuditable
	{
		public long Id { get; set; }
		public long FolderId { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }

		public string Name  { get; set; }
		public long SizeMb { get; set; }
		public string Type  { get; set; }
	}
}