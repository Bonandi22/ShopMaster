using OrderService.Application.DTOs;
using System;
using System.Threading.Tasks;

namespace OrderService.Application.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetByIdAsync(Guid id);

        Task<CartDto> GetByCustomerIdAsync(Guid customerId);

        Task AddAsync(CartDto cartDto);

        Task UpdateAsync(CartDto cartDto);

        Task DeleteAsync(Guid id);
    }
}