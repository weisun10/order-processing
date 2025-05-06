using OrderProcessing.Application.Interfaces;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.Events;
using Microsoft.Extensions.Logging;

namespace OrderProcessing.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessagePublisher _messagePublisher;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository,
                            IMessagePublisher messagePublisher,
                            ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _messagePublisher = messagePublisher;
            _logger = logger;
        }

        public async Task CreateOrderAsync(Order order)
        {
            _logger.LogInformation("Starting order creation for OrderId: {OrderId}", order.OrderId);
            try
            {
                await _orderRepository.Add(order);
                _logger.LogInformation("Order {OrderId} saved successfully.", order.OrderId);

                var totalItems = order.Items.Sum(i => i.Quantity);
                var orderCreatedEvent = new OrderCreatedEvent
                {
                    OrderId = order.OrderId,
                    TotalItems = totalItems,
                    Timestamp = DateTime.UtcNow
                };

                await _messagePublisher.Publish(orderCreatedEvent);
                _logger.LogInformation("Published OrderCreatedEvent for OrderId: {OrderId} with TotalItems: {TotalItems}",
                    order.OrderId, totalItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating order {OrderId}", order.OrderId);
                throw;
            }
        }
    }
}
