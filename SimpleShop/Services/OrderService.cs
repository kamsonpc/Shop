using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Services
{
	public class OrderService : IOrderService
	{
		private readonly UnitOfWork _unitOfWork;

		public OrderService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public List<OrdersPageVM> GetAll()
		{
			return Mapper.Map<List<Order>, List<OrdersPageVM>>(_unitOfWork.Orders.GetAll("ApplicationUser,Product").ToList());
		}

		public List<OrdersPageVM> Find(string search)
		{
			return Mapper.Map<List<Order>, List<OrdersPageVM>>(_unitOfWork.Orders.Find(s => (s.ApplicationUser.Email.Contains(search)) || (s.Product.Name.Contains(search)), "ApplicationUser,Product").ToList());
		}

		public List<OrdersPageVM> GetByUserId(string id)
		{
			return Mapper.Map<List<Order>, List<OrdersPageVM>>(_unitOfWork.Orders.Find(o => o.ApplicationUserId == id, "ApplicationUser,Product").ToList());
		}

		public ShippingVM GetShippinDataById(int id)
		{
			return Mapper.Map<Order, ShippingVM>(_unitOfWork.Orders.Find(o => o.OrderId == id).SingleOrDefault());
		}

		public void Pay(int orderId)
		{
			var orderInDb = _unitOfWork.Orders.Get(orderId);
			if(orderInDb == null) return;
			orderInDb.Payment = true;
			_unitOfWork.Complete();
		}

	}
}