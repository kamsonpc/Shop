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
			CreateMap<Product, ProductVM>();
			CreateMap<ProductVM, Product>();

			CreateMap<OrderVM, Order>();
			CreateMap<Order, OrderVM>();

			CreateMap<OrderProductUserVM, Order>();
			CreateMap<Order, OrderProductUserVM>();


		}
	}
}