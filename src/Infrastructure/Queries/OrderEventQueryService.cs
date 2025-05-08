using OrderProcessing.Application.Interfaces;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace OrderProcessing.Infrastructure.Queries
{
    public class OrderEventQueryService : IOrderEventQueryService
    {
        private readonly OrderEventDbContext _dbContext;

        public OrderEventQueryService(OrderEventDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderEvent>> GetOrderEventsAsync()
        {
            return await _dbContext.OrderEvents.ToListAsync();
        }
    }
}

