﻿using SimpleShop.Data.Models.Behaviours;
using System;

namespace SimpleShop.Data.Models.Files
{
    public class File : IEntity, IAuditable
    {
        public long Id { get; set; }
        public long FolderId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string Name { get; set; }
        public long SizeMb { get; set; }
        public string Type { get; set; }
    }
}