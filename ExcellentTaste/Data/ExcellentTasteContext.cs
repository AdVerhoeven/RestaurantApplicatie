using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExcellentTaste.Models;

namespace ExcellentTaste.Data
{
    public class ExcellentTasteContext : DbContext
    {
        public ExcellentTasteContext(DbContextOptions<ExcellentTasteContext> options)
            : base(options)
        {
        }

        public DbSet<DietTag> DietTag { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Table> Table { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<ProductTag> ProductTags { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<OrderTable> OrderTables { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductTag>().HasKey(pt => new { pt.DietTagId, pt.ProductId });

            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.DietTag)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.DietTagId);

            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<OrderTable>().HasKey(ot => new { ot.OrderId, ot.TableId });

            modelBuilder.Entity<OrderTable>()
                .HasOne(ot => ot.Order)
                .WithMany(o => o.OrderTables)
                .HasForeignKey(t => t.OrderId);

            modelBuilder.Entity<OrderTable>()
                .HasOne(ot => ot.Table)
                .WithMany(o => o.OrderTables)
                .HasForeignKey(t => t.TableId);

            modelBuilder.Entity<ProductCategory>().HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.Category)
                .HasForeignKey(pc => pc.ProductId);
        }
    }
}
