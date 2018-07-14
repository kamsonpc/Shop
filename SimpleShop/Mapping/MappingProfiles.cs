using AutoMapper;
using SimpleShop.Extensions;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Mapping
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{

			CreateMap<Product, ProductVM>().ReverseMap();

			CreateMap<OrdersPageVM, Order>().ReverseMap();

			CreateMap<ShippingVM, Order>().ReverseMap();


			CreateMap<UserAddress, UserAddressVM>();
			CreateMap<UserAddressVM, UserAddress>().ForSourceMember(x => x.Id, y => y.Ignore());

			CreateMap<CartVM, Cart>().ReverseMap();
		}
	}
}