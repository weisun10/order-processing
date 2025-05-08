using OrderProcessing.Domain.Entities;

namespace OrderProcessing.Application.Interfaces
{
    public interface IOrderQueryService
    {
        Task<List<Order>> GetOrdersAsync();
    }
}
