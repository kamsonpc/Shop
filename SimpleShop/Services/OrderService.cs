using System;
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

		public void Complete(int choosedProductId, string userId, ShippingVM shippingData)
		{
			var choosedProduct = _unitOfWork.Products.Get(choosedProductId);
			var order = new Order
			{
				ApplicationUserId = userId,
				Address = shippingData.Address,
				CityCode = shippingData.CityCode,
				Country = shippingData.Country,
				PhoneNumber = shippingData.PhoneNumber,
				NameAndSurname = shippingData.NameAndSurname,
				Date = DateTime.Now,
				Payment =  false,
				Quantity = choosedProduct.Quantity,
				ProductId = choosedProduct.ProductId,
				Price = choosedProduct.Price
			};
			_unitOfWork.Orders.Add(order);

		}

		public List<OrderVM> GetAll()
		{
			return Mapper.Map<List<Order>, List<OrderVM>>(_unitOfWork.Orders.GetAll().ToList());
		}

		public List<OrderVM> GetByUserId(string id)
		{
			return Mapper.Map<List<Order>, List<OrderVM>>(_unitOfWork.Orders.GetOrdersByUserId(id).ToList());
		}

		public ShippingVM GetShippinDataById(int id)
		{
			return Mapper.Map<Order, ShippingVM>(_unitOfWork.Orders.Find(o => o.OrderId == id).SingleOrDefault());
		}

	}
}