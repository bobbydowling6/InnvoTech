using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InnvoTech.Models
{
    public partial class BobTestContext : IdentityDbContext<ApplicationUser>
    {
        public BobTestContext() : base()
        {

        }

        public BobTestContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartProducts> CartProducts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //This is called Fluent API - it allows for more specific customization of database rules
            modelBuilder.Entity<Order>().HasKey(prop => prop.Id);
            modelBuilder.Entity<Order>()
                .Property(prop => prop.Id)
                .ValueGeneratedOnAdd();

            //Fluent API can almost be translated into a sentance:
            //Order Has Property Tracking Number whose value is generated when added
            modelBuilder.Entity<Order>().Property(prop => prop.TrackingNumber).ValueGeneratedOnAdd();
            //Order has many line items, each line item has an order, which is required

            modelBuilder.Entity<Order>().HasMany(o => o.LineItems).WithOne(l => l.Order).IsRequired();

            modelBuilder.Entity<LineItem>().HasOne(l => l.Order).WithMany(o => o.LineItems);
            modelBuilder.Entity<LineItem>().HasOne(l => l.products).WithMany(o => o.LineItems);

            modelBuilder.Entity<Products>().HasMany(p => p.LineItems).WithOne(l => l.products).IsRequired();
            base.OnModelCreating(modelBuilder);

        }
    }
}
