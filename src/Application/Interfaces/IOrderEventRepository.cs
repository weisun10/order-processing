using OrderProcessing.Domain.Entities;

namespace OrderProcessing.Application.Interfaces
{
    public interface IOrderEventRepository
    {
        Task AddAsync(OrderEvent orderEvent);
    }
}
