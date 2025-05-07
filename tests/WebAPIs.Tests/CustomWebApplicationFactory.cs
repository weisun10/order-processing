using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderProcessing.Infrastructure.Persistence;
using Moq;
using OrderProcessing.Application.Interfaces;

namespace OrderProcessing.WebAPIs.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing DbContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<OrderDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add a new in-memory database for testing
                services.AddDbContext<OrderDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryOrderDb");
                });

                // Mock dependencies
                var orderServiceMock = new Mock<IOrderService>();
                services.AddSingleton(orderServiceMock.Object);

                var orderRepositoyMock = new Mock<IOrderRepository>();
                services.AddSingleton(orderRepositoyMock.Object);

                var msgPublisherMock = new Mock<IMessagePublisher>();
                services.AddSingleton(msgPublisherMock.Object);
            });
        }
    }
}
