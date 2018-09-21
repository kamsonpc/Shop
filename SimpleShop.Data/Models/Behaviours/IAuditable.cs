using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Data.Models.Behaviours
{
    public interface IAuditable
    {
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
