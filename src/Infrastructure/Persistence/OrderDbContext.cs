using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.ValueObjects;
using System.Text.Json;

namespace OrderProcessing.Infrastructure.Persistence
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);

                entity.Property(o => o.OrderId)
                      .IsRequired();

                entity.Property(o => o.CustomerId)
                      .IsRequired();

                var orderItemsConverter = new ValueConverter<List<OrderItem>, string>(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<OrderItem>>(v, (JsonSerializerOptions?)null) ?? new List<OrderItem>());

                var orderItemsComparer = new ValueComparer<List<OrderItem>>(
                    (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (current, item) => HashCode.Combine(current, item.GetHashCode())),
                    c => c.ToList());

                entity.Property(o => o.Items)
                      .HasConversion(orderItemsConverter)
                      .Metadata.SetValueComparer(orderItemsComparer);
            });
        }
    }
}
