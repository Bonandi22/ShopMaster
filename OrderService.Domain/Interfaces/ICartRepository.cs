using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetByIdAsync(Guid id);

        Task<Cart> GetByCustomerIdAsync(Guid customerId);

        Task AddAsync(Cart cart);

        Task UpdateAsync(Cart cart);

        Task DeleteAsync(Guid id);
    }
}