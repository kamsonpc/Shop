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

			container.RegisterType<IProductService, ProductService>();
			container.RegisterType<IOrderService, OrderService>();
			container.RegisterType<ICategoryService, CategoryService>();

			container.RegisterType<ApplicationSignInManager>();
			container.RegisterType<ApplicationUserManager>();
			container.RegisterType<ApplicationDbContext>();
			container.RegisterType<AccountController>(new InjectionConstructor());
			container.RegisterType<ManageController>(new InjectionConstructor());

			//container.RegisterType<IUploadService, UploadService>();
			DependencyResolver.SetResolver(new UnityDependencyResolver(container));


		}
	}
}