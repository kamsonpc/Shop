using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Services;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace SimpleShop.Controllers
{
	public class IocUnityConfig
	{
		public static void RegisterServices()
		{
			var container = new UnityContainer();

			container.RegisterType<IProductMenagerService, ProductMenagerService>();

			container.RegisterType<ApplicationSignInManager>();
			container.RegisterType<ApplicationUserManager>();
			container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
				new InjectionConstructor(typeof(ApplicationDbContext)));
			//container.RegisterType<IUploadService, UploadService>();
			DependencyResolver.SetResolver(new UnityDependencyResolver(container));


		}
	}
}