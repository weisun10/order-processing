using OrderProcessing.Application.Interfaces;
using OrderProcessing.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace OrderProcessing.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
