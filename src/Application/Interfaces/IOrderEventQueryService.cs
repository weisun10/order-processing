using OrderProcessing.Domain.Entities;

namespace OrderProcessing.Application.Interfaces
{
    public interface IOrderEventQueryService
    {
        Task<List<OrderEvent>> GetOrderEventsAsync();
    }
}
