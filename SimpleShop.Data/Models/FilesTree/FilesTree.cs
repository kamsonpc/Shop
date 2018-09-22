using SimpleShop.Data.Models.Files;
using System;
using System.Collections.Generic;

namespace SimpleShop.Data.Models.FilesTree
{
    public class FilesTree
    {

    }

    public class Node
    {
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public long Depth { get; set; }
        public List<File> Files { get; set; }
        public List<Node> SubFolders { get; set; }
    }
}