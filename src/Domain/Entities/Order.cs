using OrderProcessing.Domain.ValueObjects;

namespace OrderProcessing.Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; private set; }

        // CustomerName -> CustomerId
        public string CustomerId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<OrderItem> Items { get; private set; } = [];

        public Order(Guid orderId, string customerId, DateTime createdAt)
        {
            OrderId = orderId;
            CustomerId = customerId;
            CreatedAt = createdAt;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }
    }
}
