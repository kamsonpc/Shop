using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SimpleShop.Interfaces;
using SimpleShop.Interfaces.Services;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Services
{
	public class CartService : ICartService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CartService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void Add(Cart cartItem)
		{
			var productInDb = _unitOfWork.Products.Get(cartItem.ProductId);
			if (cartItem.OrderedQuantity == 0 || productInDb.Quantity - cartItem.OrderedQuantity < 0) return;

			_unitOfWork.CartItems.Add(cartItem);
			_unitOfWork.Complete();
		}

		public int Counter(string userId)
		{
			return _unitOfWork.CartItems.Counter(userId);
		}

		public List<Cart> GetAll(string userId)
		{
			return _unitOfWork.CartItems.GetAll(userId).ToList();
		}

		public void Remove(int id)
		{
			var cartItemToRemove = _unitOfWork.CartItems.Get(id);
			if (cartItemToRemove == null)
				return;
			_unitOfWork.CartItems.Remove(cartItemToRemove);
			_unitOfWork.Complete();
		}

		public int Complete(string userId, ShippingVM shippingData)
		{
			var count = 0;
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

				var productInDb = _unitOfWork.Products.Get(item.ProductId);
				productInDb.Quantity -= item.OrderedQuantity;


				_unitOfWork.Orders.Add(order);
				_unitOfWork.CartItems.Remove(item);
				count++;

			}
			_unitOfWork.Complete();
			return count;
		}
	}
}