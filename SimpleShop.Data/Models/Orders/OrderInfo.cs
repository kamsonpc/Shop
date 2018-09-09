using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Data.Models.Orders
{
    public class OrderInfo
    {
        public int Id { get; set; }

        public virtual Product Product { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public bool Payment { get; set; }

        public string NameAndSurname { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string CityCode { get; set; }

        public string Country { get; set; }
    }
}