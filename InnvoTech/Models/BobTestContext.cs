using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InnvoTech.Models
{
    public partial class BobTestContext : IdentityDbContext<ApplicationUser>
    {
        public BobTestContext(): base()
        {

        }
        public BobTestContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartProducts> CartProducts { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderProducts> OrderProducts { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AspNetUserId).HasMaxLength(128);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateLastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CartProducts>(entity =>
            {
                entity.HasKey(e => new { e.CartId, e.ProductsId });

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.ProductsId).HasColumnName("ProductsID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateLastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartProducts)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartProducts_Cart");

                entity.HasOne(d => d.Products)
                    .WithMany(p => p.CartProducts)
                    .HasForeignKey(d => d.ProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartProducts_Products");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateLastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.PurchaserName)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.ShippingAddress1)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.ShippingAndHandling).HasColumnType("money");

                entity.Property(e => e.ShippingCity)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.ShippingPostalCode)
                    .IsRequired()
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.ShippingState)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SubTotal).HasColumnType("money");

                entity.Property(e => e.Tax).HasColumnType("money");

                entity.Property(e => e.TrackingNumber)
                    .IsRequired()
                    .HasColumnType("char(8)")
                    .HasDefaultValueSql("(left(newid(),(8)))");
            });

            modelBuilder.Entity<OrderProducts>(entity =>
            {
                entity.HasKey(e => new { e.ProductsId, e.OrderId });

                entity.Property(e => e.ProductsId).HasColumnName("ProductsID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateLastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PlacedName).HasMaxLength(100);

                entity.Property(e => e.PlacedUnitPrice).HasColumnType("money");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderProducts_Order");

                entity.HasOne(d => d.Products)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderProducts_Products");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateLastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.ImageUrl).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");
            });
        }
    }
}
