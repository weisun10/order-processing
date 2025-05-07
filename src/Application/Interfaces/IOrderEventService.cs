using OrderProcessing.Domain.Entities;

namespace OrderProcessing.Application.Interfaces
{
    public interface IOrderEventService
    {
        Task AddAsync(OrderEvent orderEvent);
    }
}
