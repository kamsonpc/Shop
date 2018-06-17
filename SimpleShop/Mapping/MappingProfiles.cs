using AutoMapper;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Mapping
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductVM>().ReverseMap();

			CreateMap<OrderVM, Order>().ReverseMap();

			CreateMap<OrdersPageVM, Order>().ReverseMap();

			CreateMap<ShippingVM, Order>().ReverseMap();

			CreateMap<ShippingVM, Product>().ReverseMap();

			CreateMap<CartVM, Cart>().ReverseMap();
		}
	}
}