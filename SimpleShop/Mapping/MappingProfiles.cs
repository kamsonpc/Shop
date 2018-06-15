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

			CreateMap<OrdersPageVM, Order>();
			CreateMap<Order, OrdersPageVM>();

			CreateMap<ShippingVM, Order>();
			CreateMap<Order, ShippingVM>();

			CreateMap<ShippingVM, Product>();
			CreateMap<Product, ShippingVM>();

		}
	}
}