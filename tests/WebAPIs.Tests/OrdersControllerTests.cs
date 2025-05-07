using Moq;
using Microsoft.Extensions.Logging;
using OrderProcessing.Application.Interfaces;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Presentation.WebAPIs.Controllers;
using OrderProcessing.Presentation.WebAPIs.Validation;
using Microsoft.AspNetCore.Mvc;

namespace OrderProcessing.WebAPIs.Tests
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly Mock<ILogger<OrdersController>> _mockLogger;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _mockLogger = new Mock<ILogger<OrdersController>>();
            _mockOrderService.Setup(s => s.CreateOrderAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);

            _controller = new OrdersController(_mockOrderService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateOrder_Returns201()
        {
            var request = new CreateOrderRequest
            {
                CustomerId = "12345",
                Items = new List<OrderRequestItem>
                {
                    new OrderRequestItem { ProductId = Guid.NewGuid(), Quantity = 1 },
                    new OrderRequestItem { ProductId = Guid.NewGuid(), Quantity = 2 }
                }
            };

            var result = await _controller.CreateOrder(request);

            // 201
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.NotNull(createdResult.Value);
            Assert.Equal(201, createdResult.StatusCode);
        }
    }
}
