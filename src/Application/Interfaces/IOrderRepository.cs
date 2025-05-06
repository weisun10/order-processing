using OrderProcessing.Domain.Entities;

namespace OrderProcessing.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task Add(Order order);
    }
}
