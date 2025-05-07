using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Application.Interfaces;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.ValueObjects;
using OrderProcessing.Presentation.WebAPIs.Validation;

namespace OrderProcessing.Presentation.WebAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest(new { Errors = errors });
            }

            _logger.LogInformation("Creating new order with parameters: {@Params}", request);

            var order = new Order(Guid.NewGuid(), request.CustomerId, DateTime.UtcNow);

            foreach (var item in request.Items)
            {
                order.Items.Add(new OrderItem(item.ProductId, item.Quantity));
            }

            await _orderService.CreateOrderAsync(order);
            // Return HTTP 201 Created.
            return Created("", new { id = order.OrderId });
        }
    }
}
