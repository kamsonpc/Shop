using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using SimpleShop.Interfaces;
using SimpleShop.Services;
using Unity;
using Unity.Mvc5;

namespace SimpleShop.Controllers
{
	public class IocUnityConfig
	{
		public static void RegisterServices()
		{
			var container = new UnityContainer();

			container.RegisterType<IProductMenagerService, ProductMenagerService>();
			container.RegisterType<IUploadService, UploadService>();
			DependencyResolver.SetResolver(new UnityDependencyResolver(container));


		}
	}
}