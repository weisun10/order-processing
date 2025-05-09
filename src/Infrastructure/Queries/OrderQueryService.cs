using OrderProcessing.Application.Interfaces;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace OrderProcessing.Infrastructure.Queries
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly OrderDbContext _dbContext;

        public OrderQueryService(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _dbContext.Orders
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }
    }
}
