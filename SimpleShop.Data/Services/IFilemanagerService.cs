using SimpleShop.Data.Models.FilesTree;
using System.Collections.Generic;

namespace SimpleShop.Data.Services
{
    public interface IFilemanagerService
    {
        List<Node> GetFilesTree();
    }
}