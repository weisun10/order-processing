using OrderProcessing.Domain.ValueObjects;

namespace OrderProcessing.Domain.Entities
{
    public class OrderEvent
    {
        public Guid OrderId { get; private set; }
        public int TotalItems { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ProcessedAt { get; private set; }

        public OrderEvent(Guid orderId, int totalItems, DateTime createdAt, DateTime processedAt)
        {
            OrderId = orderId;
            TotalItems = totalItems;
            CreatedAt = createdAt;
            ProcessedAt = processedAt;
        }
    }
}
