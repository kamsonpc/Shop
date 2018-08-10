//using AutoMapper;
//using SimpleShop.Areas.Admin.Models.Products;
//using SimpleShop.Areas.Client.Models.Carts;
//using SimpleShop.Areas.Client.Models.Orders;
//using SimpleShop.Areas.Client.Models.UsersAddress;
//using SimpleShop.Data.Models;

//namespace SimpleShop.Mapping
////{
////	public class MappingProfiles : Profile
////	{
////		public MappingProfiles()
////		{

////			CreateMap<OrdersPageVM, Order>().ReverseMap();

////			CreateMap<ShippingVM, Order>().ReverseMap();


////			CreateMap<UserAddress, UserAddressVM>();
////			CreateMap<UserAddressVM, UserAddress>().ForSourceMember(x => x.Id, y => y.Ignore());

////			CreateMap<CartVM, Cart>().ReverseMap();
////		}
////	}
////}