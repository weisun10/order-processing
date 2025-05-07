using Microsoft.EntityFrameworkCore.Storage;
using OrderProcessing.Domain.Entities;

namespace OrderProcessing.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
    }
}
