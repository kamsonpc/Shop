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
		private readonly IProductService _product;
		public OrderService(IProductService product)
		{
			_product = product;
		}

		public List<OrderVM> GetAll()
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var orders = ctx.Orders.Include(m => m.Product).ToList();
				return Mapper.Map<List<Order>, List<OrderVM>>(orders);
			}
		}

		public bool AddNew(ProductVM choosedProduct, ShippingVM shippingData, string userId)
		{

			var product = _product.GetById(choosedProduct.ProductId);

			if (product == null)
			{
				return false;
			}

			Order order = new Order
			{
				ApplicationUserId = userId,
				ProductId = product.ProductId,
				Date = DateTime.Now,
				Price = product.Price * choosedProduct.CustomerQuantity,
				Quantity = choosedProduct.CustomerQuantity,
				Payment = false,
				NameAndSurname = shippingData.NameAndSurname,
				PhoneNumber = shippingData.PhoneNumber,
				Address = shippingData.Address,
				CityCode = shippingData.CityCode,
				Country = shippingData.Country
			};
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				ctx.Orders.Add(order);
				ctx.SaveChanges();
			}
			return true;
		}

		public OrderVM GetById(int id)
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var order = ctx.Orders.SingleOrDefault(o => o.OrderId == id);
				var orderVm = Mapper.Map<Order, OrderVM>(order);
				return orderVm;
			}
		}

		public ShippingVM GetShippingById(int id)
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var order = ctx.Orders.SingleOrDefault(o => o.OrderId == id);
				var orderVm = Mapper.Map<Order, ShippingVM>(order);
				return orderVm;
			}
		}

		public List<OrderProductUserVM> GetOrdersByUser(string id)
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var orders = ctx.Orders.Where(b => b.ApplicationUserId == id).Include(m => m.Product).ToList();
				var ordersVm = Mapper.Map<List<Order>, List<OrderProductUserVM>>(orders);
				return ordersVm;
			}
		}

		public List<OrderProductUserVM> GetAllOrders()
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var orders = ctx.Orders.Include(m => m.Product).Include(x => x.ApplicationUser).OrderBy(d => d.Date)
					.ToList();
				var ordersVm = Mapper.Map<List<Order>, List<OrderProductUserVM>>(orders);
				return ordersVm;
			}
		}

		public bool ChangePayment(int id)
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var order = ctx.Orders.Where( o => o.OrderId == id).SingleOrDefault();
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

					ctx.SaveChanges();
					return true;
				}
				else
				{
					return false;
				}				
			}
		}

		public List<OrderProductUserVM> SearchByName(string query)
		{
			using (ApplicationDbContext ctx = new ApplicationDbContext())
			{
				var orders = ctx.Orders.ToList().FindAll(m => m.ApplicationUser.UserName.Contains(query));
				return Mapper.Map<List<Order>, List<OrderProductUserVM>>(orders);
			}
		}

	}
}