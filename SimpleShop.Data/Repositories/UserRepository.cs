using SimpleShop.Data.Interfaces.Repositories;
using SimpleShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleShop.Data.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        { }

    }
}