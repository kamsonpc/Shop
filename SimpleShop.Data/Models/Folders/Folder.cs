using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleShop.Data.Models.Behaviours;

namespace SimpleShop.Data.Models.Folders
{
	public class Folder : IEntity,IAuditable
	{
		public long Id { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string Name { get; set; }
		public long ParentId { get; set; }
		public long Depth { get; set; }
	}

	public class FolderItem
	{
		public long Id { get; set; }
		public long ParentId { get; set; }
		public long Depth { get; set; }
		public string Name { get; set; }
		public List<FolderItem> Childs { get; set; }

	}
}