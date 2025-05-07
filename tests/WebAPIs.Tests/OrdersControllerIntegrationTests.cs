using System.Text;
using System.Text.Json;
using System.Net;
using OrderProcessing.Presentation.WebAPIs.Validation;

namespace OrderProcessing.WebAPIs.Tests
{
    public class OrdersControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrdersControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateOrder_InvalidCustomerId_ReturnsBadRequest()
        {
            var invalidRequest = new CreateOrderRequest
            {
                CustomerId = "",
                Items = new List<OrderRequestItem>
                {
                    new OrderRequestItem { ProductId = Guid.NewGuid(), Quantity = 1 },
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(invalidRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/orders", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("errors", responseContent);
            Assert.Contains("The CustomerId field is required", responseContent);
        }

        [Fact]
        public async Task CreateOrder_DuplicateProductId_ReturnsBadRequest()
        {
            var productId = Guid.NewGuid();
            var invalidRequest = new CreateOrderRequest
            {
                CustomerId = "1234",
                Items = new List<OrderRequestItem>
                {
                    new OrderRequestItem { ProductId = productId, Quantity = 1 },
                    new OrderRequestItem { ProductId = productId, Quantity = 1 }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(invalidRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/orders", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("errors", responseContent);
            Assert.Contains("Duplicate ProductId found in items.", responseContent);
        }
    }
}
