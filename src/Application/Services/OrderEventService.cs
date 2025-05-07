using OrderProcessing.Application.Interfaces;
using OrderProcessing.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace OrderProcessing.Application.Services
{
    public class OrderEventService : IOrderEventService
    {
        private readonly IOrderEventRepository _orderEventRepository;
        private readonly ILogger<OrderEventService> _logger;

        public OrderEventService(IOrderEventRepository orderEventRepository,
                                  ILogger<OrderEventService> logger)
        {
            _orderEventRepository = orderEventRepository;
            _logger = logger;
        }

        public async Task AddAsync(OrderEvent orderEvent)
        {
            _logger.LogInformation("Starting to save order event for OrderId: {OrderId}", orderEvent.OrderId);
            try
            {
                await _orderEventRepository.AddAsync(orderEvent);
                _logger.LogInformation("OrderEvent for OrderId: {OrderId} saved successfully.", orderEvent.OrderId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving order event for OrderId: {OrderId}", orderEvent.OrderId);
                throw;
            }
        }
    }
}
