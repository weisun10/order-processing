using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.Events;
using OrderProcessing.Infrastructure.Persistence;
using Rebus.Handlers;
using Microsoft.Extensions.Logging;

namespace OrderProcessing.Presentation.OrderEventService.Handlers
{    public class OrderCreatedEventHandler : IHandleMessages<OrderCreatedEvent>
    {
        private readonly OrderEventDbContext _dbContext;
        private readonly ILogger<OrderCreatedEventHandler> _logger;

        public OrderCreatedEventHandler(OrderEventDbContext dbContext, ILogger<OrderCreatedEventHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task Handle(OrderCreatedEvent message)
        {
            _logger.LogInformation("Receiving OrderCreatedEvent for OrderId: {OrderId}", message.OrderId);

            var orderEvent = new OrderEvent(message.OrderId, message.TotalItems, message.Timestamp, DateTime.UtcNow);
            _dbContext.OrderEvents.Add(orderEvent);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Saved OrderEvent for OrderId: {OrderId}, TotalItems: {TotalItems}", message.OrderId, message.TotalItems);
        }
    }
}
