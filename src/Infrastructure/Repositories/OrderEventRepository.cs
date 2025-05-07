using OrderProcessing.Application.Interfaces;
using OrderProcessing.Domain.Entities;

namespace OrderProcessing.Infrastructure.Persistence
{
    public class OrderEventRepository : IOrderEventRepository
    {
        private readonly OrderEventDbContext _dbContext;

        public OrderEventRepository(OrderEventDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(OrderEvent orderEvent)
        {
            _dbContext.OrderEvents.Add(orderEvent);
            await _dbContext.SaveChangesAsync();
        }
    }
}
