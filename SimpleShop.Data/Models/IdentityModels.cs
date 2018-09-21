using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleShop.Data.Models;
using SimpleShop.Data.Models.Behaviours;
using SimpleShop.Data.Models.Orders;
using System.Security.Claims;
using System.Threading;
using System.Web;
using SimpleShop.Data.Models.Folders;


namespace SimpleShop.Data.Models
{

// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
public class ApplicationUser : IdentityUser
	{
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}

		public virtual UserAddress Address { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<Cart> Cart { get; set; }

	}

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{

		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Cart> CartItems { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<UserAddress> UserAddress { get; set; }
		public DbSet<Folder> Folders { get; set; }


		public override int SaveChanges()
		{
			var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
			var userId = HttpContext.Current.User.Identity.GetUserId();
			var added = ChangeTracker.Entries<IAuditable>().Where(E => E.State == EntityState.Added).ToList();

			added.ForEach(E =>
			{
				E.Property(x => x.CreatedDate).CurrentValue = DateTime.UtcNow;
				E.Property(x => x.CreatedBy).CurrentValue = userId;

				E.Property(x => x.CreatedDate).IsModified = true;
				E.Property(x => x.CreatedBy).IsModified = true;
			});

			var modified = ChangeTracker.Entries<IAuditable>().Where(E => E.State == EntityState.Modified).ToList();

			modified.ForEach(E =>
			{
				E.Property(x => x.ModifiedDate).CurrentValue = DateTime.UtcNow;
				E.Property(x => x.ModifiedDate).IsModified = true;

				E.Property(x => x.CreatedDate).CurrentValue = E.Property(x => x.CreatedDate).OriginalValue;
				E.Property(x => x.CreatedDate).IsModified = false;

				E.Property(x => x.CreatedBy).CurrentValue = E.Property(x => x.CreatedBy).OriginalValue;
				E.Property(x => x.CreatedBy).IsModified = false;
			});

			return base.SaveChanges();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ApplicationUser>()
				.HasOptional(u => u.Address) 
				.WithRequired(ud => ud.ApplicationUser); 
			base.OnModelCreating(modelBuilder);
		}
		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

	}
}