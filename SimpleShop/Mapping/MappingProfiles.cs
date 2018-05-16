using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Mapping
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductViewModel>();
			CreateMap<ProductViewModel, Product>();

			CreateMap<OrderViewModel, Order>();
			CreateMap<Order, OrderViewModel>();

			CreateMap<OrderProductViewModel, Order>();
			CreateMap<Order, OrderProductViewModel>();


		}
	}
}