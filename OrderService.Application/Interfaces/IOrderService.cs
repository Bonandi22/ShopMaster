using OrderService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> GetByIdAsync(Guid id);

        Task<IEnumerable<OrderDto>> GetAllAsync();

        Task AddAsync(OrderDto orderDto);

        Task UpdateAsync(OrderDto orderDto);

        Task DeleteAsync(Guid id);
    }
}