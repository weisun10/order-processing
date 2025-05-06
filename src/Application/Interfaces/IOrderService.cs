using OrderProcessing.Domain.Entities;

namespace OrderProcessing.Application.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
    }
}
