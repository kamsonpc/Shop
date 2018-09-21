using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using SimpleShop.Data.Interfaces.Repositories;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.Folders;

namespace SimpleShop.Data.Repositories
{
	public class FolderRepository : Repository<Folder>, IFolderRepository
	{
		public FolderRepository(DbContext context) : base(context)
		{
		}

		public IEnumerable<FolderItem> GetAll()
		{

			var folders = _contex.Folders.ToList();

			var listFolders = new List<FolderItem>();
			foreach (var folder in folders)
			{

				listFolders.Add(new FolderItem { Id = folder.Id, ParentId = folder.ParentId, Name = folder.Name });
			}

			var rootNodes = new List<FolderItem>();
			foreach (var node in listFolders)
			{
				var parent = listFolders.Find(i => i.Id == node.ParentId);
				if (parent == null)
				{
					rootNodes.Add(node);
				}
				else
				{
					if (parent.Childs == null)
						parent.Childs = new List<FolderItem>();
					parent.Childs.Add(node);
				}
			}
			return rootNodes;
		}



		public ApplicationDbContext _contex
		{
			get { return Contex as ApplicationDbContext; }
		}
	}
}