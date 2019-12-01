using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClydesTakeoutPrototype.Models.MenuModels;
using ClydesTakeoutPrototype.Models.OrderModels;
using ClydesTakeoutPrototype.Models.SystemModels;

namespace ClydesTakeoutPrototype.Data
{
    /// <summary>
    /// [Deprecated in Current Configuration] - Handles connection to the localdb instance storing User, Order, and Menu data.
    /// Managed by the dependency injection container. 
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) 
            : base(options)
        {
        }

        /// <summary>
        /// A set to store Items
        /// </summary>
        public DbSet<Item> Item { get; set; }
        /// <summary>
        /// A set to store Orders
        /// </summary>
        public DbSet<Order> Order { get; set; }
        /// <summary>
        /// A set to store Menus
        /// </summary>
        public DbSet<Menu> Menu { get; set; }
        /// <summary>
        /// A set to store Users
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Override base to configure entity relationships. 
        /// </summary>
        /// <param name="builder">A ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Entree>();
            builder.Entity<Side>();
            builder.Entity<Drink>();

            builder.Entity<Order>()
                .HasMany(i => i.Items)
                .WithOne();

            builder.Entity<Menu>()
                .HasMany(i => i.Items)
                .WithOne();

            //builder.Entity<User>()
            //    .HasMany(o => o.PendingOrders)
            //    .WithOne();

            base.OnModelCreating(builder);
        }

    }
}
