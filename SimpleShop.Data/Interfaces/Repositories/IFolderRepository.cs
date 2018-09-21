using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleShop.Data.Models.Folders;

namespace SimpleShop.Data.Interfaces.Repositories
{
	public interface IFolderRepository
	{
		IEnumerable<FolderItem> GetAll();
	}
}