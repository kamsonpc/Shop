﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SimpleShop.Models;

[assembly: OwinStartupAttribute(typeof(SimpleShop.Startup))]
namespace SimpleShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
			CreateAdmin();
        }

	    private void CreateAdmin()
	    {
		    ApplicationDbContext context = new ApplicationDbContext();

		    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
		    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

			if (!roleManager.RoleExists("Administrator"))
		    {
			    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
			    role.Name = "Administrator";
			    roleManager.Create(role);

				var user = new ApplicationUser();
			    user.UserName = "admin@gmail.com";
			    user.Email = "admin@gmail.com";

			    string userPass = "zaq1@WSX";

			    var chkUser = UserManager.Create(user, userPass);
			    if (chkUser.Succeeded)
			    {
				    UserManager.AddToRole(user.Id, "Administrator");

			    }
			}
		}
    }
	

}
