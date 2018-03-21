using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProductHierarchy.Logic
{
    public partial class ProductHierarchyContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Rebate> Rebates { get; set; }

        public virtual DbSet<ProductHierarchy> ProductHierarchies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Demonstrating how to add a logger to Entity Framework
                var lf = new LoggerFactory();
                lf.AddProvider(new DebugLoggerProvider());
                optionsBuilder.UseLoggerFactory(lf);

                // Connect to SQL Server
                optionsBuilder.UseSqlServer(@"Server=...;Database=ProductHierarchy;User=reader;...");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map classes to DB structure - this has to be done manually because we use
            // an existing database and do not let Entity Framework maintain the DB structure.

            // Read more about working with existing DBs in
            // https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db#reverse-engineer-your-model

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ProductNumber).IsRequired();
                entity.Property(e => e.Manufacturer).IsRequired();
            });

            modelBuilder.Entity<Rebate>(entity =>
            {
                entity.ToTable("Rebate");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.MinQuantity);
                entity.Property(e => e.RebatePerc);
                entity.HasOne(e => e.Product)
                    .WithMany(p => p.Rebates)
                    .HasForeignKey(d => d.ProductID)
                    .IsRequired();
            });

            modelBuilder.Entity<ProductHierarchy>(entity =>
            {
                entity.ToTable("ProductHierarchy");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Amount);
                entity.HasOne(e => e.ParentProduct)
                    .WithMany(p => p.ChildProducts)
                    .HasForeignKey(d => d.ParentProductID)
                    .IsRequired();
                entity.HasOne(e => e.ChildProduct)
                    .WithMany(p => p.ParentProducts)
                    .HasForeignKey(d => d.ChildProductID)
                    .IsRequired();
            });
        }
    }
}
