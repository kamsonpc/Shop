﻿using SimpleShop.Data.Models.Behaviours;
using System;

namespace SimpleShop.Data.Models.Folders
{
    public class Folder : IEntity, IAuditable
    {
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public long Depth { get; set; }
    }

}