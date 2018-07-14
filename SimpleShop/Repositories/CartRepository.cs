using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SimpleShop.Interfaces.Repositories;
using SimpleShop.Models;

namespace SimpleShop.Repositories
{
	public class CartRepository : Repository<Cart>,ICartRepository
	{

		public CartRepository(DbContext context) : base(context)
		{
			
		}

		public new IEnumerable<Cart> Find(Expression<Func<Cart, bool>> predicate)
		{
			return ApplicationDbContext.CartItems.Include(p => p.Product).Include(a => a.ApplicationUser).Where(predicate);
		}

		public new IEnumerable<Cart> GetAll()
		{
			return ApplicationDbContext.CartItems.Include(p => p.Product).Include(a => a.ApplicationUser);
		}

		public IEnumerable<Cart> GetAll(string userId)
		{
			return ApplicationDbContext.CartItems.Include(p => p.Product);
		}

		public int Counter(string userId)
		{
			return Find(c => c.ApplicationUserId == userId).Count();
		}

		public ApplicationDbContext ApplicationDbContext
		{
			get { return Contex as ApplicationDbContext; }
		}

	}
}