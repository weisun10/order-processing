using Microsoft.EntityFrameworkCore;
using OrderProcessing.Domain.Entities;

namespace OrderProcessing.Infrastructure.Persistence
{
    public class OrderEventDbContext : DbContext
    {
        public OrderEventDbContext(DbContextOptions<OrderEventDbContext> options)
            : base(options)
        {
        }

        public DbSet<OrderEvent> OrderEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderEvent>(entity =>
            {
                // Configure primary key and properties.
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId)
                    .IsRequired();

                entity.Property(e => e.TotalItems)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.ProcessedAt)
                    .IsRequired();
            });
        }
    }
}
