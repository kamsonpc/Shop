using System.Web.Mvc;
using SimpleShop.Controllers;
using SimpleShop.Interfaces;
using SimpleShop.Interfaces.Repositories;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models;
using SimpleShop.Repositories;
using SimpleShop.Services;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace SimpleShop
{
	public class IocUnityConfig
	{
		public static void RegisterServices()
		{
			var container = new UnityContainer();

			container.RegisterType<ICategoryRepository, CategoryRepository>();
			container.RegisterType<IProductService, ProductService>();
			container.RegisterType<ICartService, CartService>();
			container.RegisterType<IOrderService, OrderService>();
			container.RegisterType<IUnitOfWork, UnitOfWork>();


			container.RegisterType<ApplicationSignInManager>();
			container.RegisterType<ApplicationUserManager>();
			container.RegisterType<AccountController>(new InjectionConstructor());
			container.RegisterType<ManageController>(new InjectionConstructor());

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));


		}
	}
}