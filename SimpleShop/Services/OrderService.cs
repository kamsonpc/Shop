using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using SimpleShop.Interfaces;
using SimpleShop.Models;
using SimpleShop.Models.ViewsModels;

namespace SimpleShop.Services
{
	public class OrderService : IOrderService
	{
		private readonly IApplicationDbContex _applicationDb;
		public OrderService(IApplicationDbContex applicationDb)
		{
			_applicationDb = applicationDb;
		}

		public List<OrderVM> GetAll()
		{
			var orders = _applicationDb.Orders.Include(m => m.Product).ToList();
			return Mapper.Map<List<Order>,List<OrderVM>>(orders);
		}

		public void AddNew(Order order)
		{
			_applicationDb.Orders.Add(order);
			_applicationDb.SaveChanges();
		}

		public OrderVM GetById(int id)
		{
			var order = _applicationDb.Orders.SingleOrDefault(o => o.OrderId == id);
			var orderVm = Mapper.Map<Order, OrderVM>(order);  
			return orderVm;
		}
		public ShippingVM GetShippingById(int id)
		{
			var order = _applicationDb.Orders.SingleOrDefault(o => o.OrderId == id);
			var orderVm = Mapper.Map<Order, ShippingVM>(order);
			return orderVm;
		}

		public List<OrderProductUserVM> GetOrdersByUser(string id)
		{
			var orders = _applicationDb.Orders.Where(b => b.ApplicationUserId == id).Include(m => m.Product).ToList();
			var ordersVm = Mapper.Map<List<Order>, List<OrderProductUserVM>>(orders);
			return ordersVm;

		}

		public List<OrderProductUserVM> GetAllOrders()
		{
			var orders = _applicationDb.Orders.Include(m => m.Product).Include(x => x.ApplicationUser).OrderBy(d => d.Date).ToList();
			var ordersVm = Mapper.Map<List<Order>, List<OrderProductUserVM>>(orders);
			return ordersVm;

		}

		public bool ChangePayment(int id)
		{
			var order = GetById(id);
			if (order != null)
			{
				if (order.Payment)
				{
					order.Payment = false;
				}
				else
				{
					order.Payment = true;
				}
				_applicationDb.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public List<OrderProductUserVM> SearchByName(string query)
		{
			var orders =_applicationDb.Orders.ToList().FindAll(m => m.ApplicationUser.UserName.Contains(query));
			return Mapper.Map<List<Order>,List<OrderProductUserVM>>(orders);
		}
	}
}