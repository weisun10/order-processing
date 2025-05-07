using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.Events;
using Rebus.Handlers;
using Microsoft.Extensions.Logging;
using OrderProcessing.Application.Interfaces;

namespace OrderProcessing.Presentation.OrderEventService.Handlers
{
    public class OrderCreatedEventHandler : IHandleMessages<OrderCreatedEvent>
    {
        private readonly IOrderEventRepository _orderEventRepository;
        private readonly ILogger<OrderCreatedEventHandler> _logger;

        public OrderCreatedEventHandler(IOrderEventRepository orderEventRepository, ILogger<OrderCreatedEventHandler> logger)
        {
            _orderEventRepository = orderEventRepository;
            _logger = logger;
        }

        public async Task Handle(OrderCreatedEvent message)
        {
            _logger.LogInformation("Receiving OrderCreatedEvent for OrderId: {OrderId}", message.OrderId);

            var orderEvent = new OrderEvent(message.OrderId, message.TotalItems, message.Timestamp, DateTime.UtcNow);
            await _orderEventRepository.AddAsync(orderEvent);

            _logger.LogInformation("Saved OrderEvent for OrderId: {OrderId}, TotalItems: {TotalItems}", message.OrderId, message.TotalItems);
        }
    }
}
