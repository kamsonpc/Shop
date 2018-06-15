using System.Web.Mvc;
using SimpleShop.Interfaces.Services;
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

			//container.RegisterType<IProductRepository, ProductRepository>();
			//container.RegisterType<IOrderRepository, OrderRepository>();
			//container.RegisterType<ICategoryRepository, CategoryRepository>();
			container.RegisterType<IProductService, ProductService>();
			container.RegisterType<IOrderService, OrderService>();
			container.RegisterType<ICategoryService, CategoryService>();


			container.RegisterType<ApplicationSignInManager>();
			container.RegisterType<ApplicationUserManager>();
			container.RegisterType<AccountController>(new InjectionConstructor());
			container.RegisterType<ManageController>(new InjectionConstructor());

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));


		}
	}
}