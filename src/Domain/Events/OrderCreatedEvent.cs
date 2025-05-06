using OrderProcessing.Domain.ValueObjects;

namespace OrderProcessing.Domain.Events
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public int TotalItems { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
