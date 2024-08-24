using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using CustomerService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CustomerDbContext _context;

        public AddressRepository(CustomerDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Address> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid address ID.", nameof(id));
            }

            return await _context.Addresses
                .AsNoTracking() // Ensure not to track the address entity
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _context.Addresses
                .AsNoTracking() // Ensure not to track the address entities
                .ToListAsync();
        }

        public async Task AddAsync(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address), "Address cannot be null.");
            }

            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address), "Address cannot be null.");
            }

            var existingAddress = await _context.Addresses.FindAsync(address.Id);
            if (existingAddress == null)
            {
                throw new KeyNotFoundException($"Address with ID {address.Id} not found.");
            }

            _context.Entry(existingAddress).CurrentValues.SetValues(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid address ID.", nameof(id));
            }

            var address = await _context.Addresses.FindAsync(id);
            if (address != null)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Address with ID {id} not found.");
            }
        }
    }
}