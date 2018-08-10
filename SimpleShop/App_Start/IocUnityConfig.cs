using System.Web.Mvc;
using SimpleShop.Areas.Client.Controllers;
using SimpleShop.Data.Interfaces;
using SimpleShop.Data.Interfaces.Repositories;
using SimpleShop.Data.Interfaces.Services;
using SimpleShop.Data.Models;
using SimpleShop.Data.Repositories;
using SimpleShop.Data.Services;
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
			container.RegisterType<ICategoriesService, CategoriesService>();
			container.RegisterType<IUnitOfWork, UnitOfWork>();


			container.RegisterType<ApplicationSignInManager>();
			container.RegisterType<ApplicationUserManager>();
			container.RegisterType<AccountController>(new InjectionConstructor());
			container.RegisterType<ManageController>(new InjectionConstructor());

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));


		}
	}
}