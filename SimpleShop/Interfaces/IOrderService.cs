﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleShop.Models;

namespace SimpleShop.Interfaces
{
	public interface IOrderService
	{
		void AddNew(Order order);
		List<Order> GetOrdersByUser(string id);
		Order GetDetails(int id);
	}
}