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
using SimpleShop.Repositories;
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

			container.RegisterType<IProductRepository, ProductRepository>();
			container.RegisterType<IOrderRepository, OrderRepository>();
			container.RegisterType<ICategoryRepository, CategoryRepository>();

			container.RegisterType<ApplicationSignInManager>();
			container.RegisterType<ApplicationUserManager>();
			container.RegisterType<AccountController>(new InjectionConstructor());
			container.RegisterType<ManageController>(new InjectionConstructor());

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));


		}
	}
}