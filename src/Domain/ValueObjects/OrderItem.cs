namespace OrderProcessing.Domain.ValueObjects
{
    public record OrderItem(Guid ProductId, int Quantity);
}
