using Microsoft.EntityFrameworkCore;
using ShippingService.Domain.Entities;
using ShippingService.Domain.Interfaces;
using ShippingService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShippingService.Infrastructure.Repositories
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly ShippingDbContext _context;

        public ShippingRepository(ShippingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Shipping> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid shipping ID.", nameof(id));
            }

            try
            {
                return await _context.Shippings.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException($"An error occurred while retrieving shipping with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<Shipping>> GetAllAsync()
        {
            try
            {
                return await _context.Shippings.ToListAsync();
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException("An error occurred while retrieving all shippings.", ex);
            }
        }

        public async Task AddAsync(Shipping shipping)
        {
            if (shipping == null)
            {
                throw new ArgumentNullException(nameof(shipping), "Shipping cannot be null.");
            }

            try
            {
                await _context.Shippings.AddAsync(shipping);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException("An error occurred while adding shipping.", ex);
            }
        }

        public async Task UpdateAsync(Shipping shipping)
        {
            if (shipping == null)
            {
                throw new ArgumentNullException(nameof(shipping), "Shipping cannot be null.");
            }

            try
            {
                _context.Shippings.Update(shipping);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException("An error occurred while updating shipping.", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid shipping ID.", nameof(id));
            }

            try
            {
                var shipping = await GetByIdAsync(id);
                if (shipping == null)
                {
                    throw new KeyNotFoundException($"Shipping with ID {id} not found.");
                }

                _context.Shippings.Remove(shipping);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException($"An error occurred while deleting shipping with ID {id}.", ex);
            }
        }
    }
}