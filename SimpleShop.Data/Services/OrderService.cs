using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SimpleShop.Data.Interfaces;
using SimpleShop.Data.Interfaces.Services;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.Orders;

namespace SimpleShop.Data.Services
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;

		public OrderService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public List<OrderInfo> Find(string search)
		{
			var orders = GetAll();
			return orders.FindAll(s => s.ProductName.Contains(search));
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

        public List<OrderInfo> GetAll()
        {
            var order = _unitOfWork.Orders.GetAll();
            var products = _unitOfWork.Products.GetAll();
            return order
                .Join(products, x => x.ProductId, o => o.ProductId, (o, x) => 
                new OrderInfo
                {
                    Id = o.OrderId,
                    Quantity = o.Quantity,
					Date = o.Date,
					Price = o.Price,
					ProductName = x.Name
                })
                .ToList();
        }
    }
}