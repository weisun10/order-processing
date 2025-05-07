using Microsoft.Extensions.Logging;
using Moq;
using OrderProcessing.Application.Interfaces;
using OrderProcessing.Application.Services;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.Events;

namespace OrderProcessing.Application.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task CreateOrderAsync_SavesOrderAndPublishEvent()
        {
            // mock dependent interfaces
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var msgPublisherMock = new Mock<IMessagePublisher>();
            var loggerMock = new Mock<ILogger<OrderService>>();

            orderRepositoryMock.Setup(p => p.AddAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);

            // create OrderService instance
            var orderService = new OrderService(orderRepositoryMock.Object, msgPublisherMock.Object, loggerMock.Object);

            // invoke CreateOrderAsync method
            var order = new Order(Guid.NewGuid(), "customer id", DateTime.UtcNow);
            order.AddItem(new(Guid.NewGuid(), 2));
            order.AddItem(new(Guid.NewGuid(), 3));

            await orderService.CreateOrderAsync(order);

            // verify IOrderRepositoy.AddAsync is called with expected Order entity
            orderRepositoryMock.Verify(x => x.AddAsync(
                It.Is<Order>(e =>
                    e.OrderId == order.OrderId &&
                    e.CustomerId == "customer id" &&
                    e.CreatedAt <= DateTime.UtcNow &&
                    e.Items.Count == 2 &&
                    e.Items[0].Quantity == 2 &&
                    e.Items[1].Quantity == 3
                )),
                Times.Once);

            // verify IMessagePublisher.Publish is called with expected OrderCreatedEvent event
            msgPublisherMock.Verify(x => x.Publish(
                It.Is<OrderCreatedEvent>(e =>
                    e.OrderId == order.OrderId &&
                    e.TotalItems == 5
                )),
                Times.Once);
        }
    }
}
