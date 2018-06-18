using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Services
{
	public class CartService : ICartService
	{
		private readonly UnitOfWork _unitOfWork;

		public CartService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void Add(CartVM cartItemVm)
		{
			var cartItem = Mapper.Map<CartVM, Cart>(cartItemVm);
			_unitOfWork.CartItems.Add(cartItem);
			_unitOfWork.Complete();
		}

		public int Counter(string userId)
		{
			return _unitOfWork.CartItems.Counter(userId);
		}

		public List<CartVM> GetAll(string userId)
		{
			return Mapper.Map<List<Cart>, List<CartVM>>(_unitOfWork.CartItems.GetAll(userId).ToList());
		}

		public void Remove(int id)
		{
			var cartItemToRemove = _unitOfWork.CartItems.Get(id);
			if (cartItemToRemove == null)
				return;
			_unitOfWork.CartItems.Remove(cartItemToRemove);
			_unitOfWork.Complete();
		}

		public void Complete(string userId, ShippingVM shippingData)
		{
			var items = _unitOfWork.CartItems.Find(c => c.ApplicationUserId == userId).ToList();
			foreach (var item in items)
			{
				var order = new Order
				{   
					ApplicationUserId = userId,
					ProductId = item.ProductId,
					Date = DateTime.Now,
					Price = item.Product.Price * item.OrderedQuantity,
					Quantity = item.OrderedQuantity,
					Address = shippingData.Address,
					Country = shippingData.Country,
					CityCode = shippingData.CityCode,
					PhoneNumber = shippingData.PhoneNumber,
					NameAndSurname = shippingData.NameAndSurname
				};
				var productInDb =_unitOfWork.Products.Get(item.ProductId);
				productInDb.Quantity -= item.OrderedQuantity;

				_unitOfWork.Orders.Add(order);
				_unitOfWork.CartItems.Remove(item);
			}
			_unitOfWork.Complete();
		}
	}
}