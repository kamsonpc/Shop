using System.Collections.Generic;
using System.Linq;
using SimpleShop.Data.Interfaces;
using SimpleShop.Data.Interfaces.Services;
using SimpleShop.Data.Models;

namespace SimpleShop.Data.Services
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;

		public OrderService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public List<Order> GetAll()
		{
			return _unitOfWork.Orders.GetAll().ToList();
		}

		public List<Order> Find(string search)
		{
			return _unitOfWork.Orders.Find(s => (s.ApplicationUser.Email.Contains(search)) || s.Product.Name.Contains(search)).ToList();
		}

		public List<Order> GetByUserId(string id)
		{
			return _unitOfWork.Orders.GetOrdersByUserId(id).ToList();
		}

		public Order GetShippinDataById(int id)
		{
			return _unitOfWork.Orders.Find(o => o.OrderId == id).SingleOrDefault();
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