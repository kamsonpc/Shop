using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Data.Models.Orders
{
    public class OrderInfo
    {
        public int Id { get; set; }

	    public string ProductName { get; set; }

        public decimal Price { get; set; }

		public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public bool Payment { get; set; }
    }
}