using SimpleShop.Data.Interfaces;
using SimpleShop.Data.Models.FilesTree;
using System.Collections.Generic;
using System.Linq;

namespace SimpleShop.Data.Services
{
    public class FilemanagerService : IFilemanagerService
    {
        private readonly IUnitOfWork _unitOfWork;

        #region Ctor
        public FilemanagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region GetFilesTree()
        public List<Node> GetFilesTree()
        {
            var folders = _unitOfWork.Folders.GetAll().ToList();
            var files = _unitOfWork.Files.GetAll().ToList();

            var folderTree = new List<Node>();

            foreach (var folder in folders)
            {
                folderTree.Add(new Node
                {
                    CreatedBy = folder.CreatedBy,
                    CreatedDate = folder.CreatedDate,
                    Depth = folder.Depth,
                    ModifiedDate = folder.ModifiedDate,
                    ModifiedBy = folder.ModifiedBy,
                    Id = folder.Id,
                    Name = folder.Name,
                    ParentId = folder.ParentId,
                    Files = files.Where(x => x.FolderId == folder.Id).ToList()
                });
            }

            var rootNodes = new List<Node>();

            foreach (var node in folderTree)
            {
                var parent = folderTree.Find(i => i.Id == node.ParentId);
                if (parent == null)
                {
                    rootNodes.Add(node);
                }
                else
                {
                    if (parent.SubFolders == null)
                        parent.SubFolders = new List<Node>();
                    parent.SubFolders.Add(node);
                }
            }
            return rootNodes;
        }
        #endregion
    }
}