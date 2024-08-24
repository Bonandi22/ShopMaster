using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly OrderDbContext _context;

        public CartRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetByIdAsync(Guid id)
        {
            return await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cart> GetByCustomerIdAsync(Guid customerId)
        {
            return await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var cart = await GetByIdAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }
    }
}