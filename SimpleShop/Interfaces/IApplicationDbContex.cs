using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SimpleShop.Models;

namespace SimpleShop.Interfaces
{
	public interface IApplicationDbContex : IDisposable
	{
		DbSet<Product> Products { get; }
		DbSet<Order> Orders { get;}
		DbSet<Category> Categories { get; }
		int SaveChanges();
	}
}