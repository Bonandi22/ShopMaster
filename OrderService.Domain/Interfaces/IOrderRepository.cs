using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(Guid id);

        Task<IEnumerable<Order>> GetAllAsync();

        Task AddAsync(Order order);

        Task UpdateAsync(Order order);

        Task DeleteAsync(Guid id);
    }
}