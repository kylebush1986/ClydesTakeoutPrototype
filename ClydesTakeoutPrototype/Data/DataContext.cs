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
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) 
            : base(options)
        {
        }

        public DbSet<Item> Item { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<User> User { get; set; }

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
