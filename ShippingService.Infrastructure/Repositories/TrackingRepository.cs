using Microsoft.EntityFrameworkCore;
using ShippingService.Domain.Entities;
using ShippingService.Domain.Interfaces;
using ShippingService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingService.Infrastructure.Repositories
{
    public class TrackingRepository : ITrackingRepository
    {
        private readonly ShippingDbContext _context;

        public TrackingRepository(ShippingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Tracking> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid tracking ID.", nameof(id));
            }

            try
            {
                return await _context.Trackings.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException($"An error occurred while retrieving tracking with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<Tracking>> GetByShippingIdAsync(Guid shippingId)
        {
            if (shippingId == Guid.Empty)
            {
                throw new ArgumentException("Invalid shipping ID.", nameof(shippingId));
            }

            try
            {
                return await _context.Trackings
                                     .Where(t => t.ShippingId == shippingId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException($"An error occurred while retrieving trackings for shipping ID {shippingId}.", ex);
            }
        }

        public async Task AddAsync(Tracking tracking)
        {
            if (tracking == null)
            {
                throw new ArgumentNullException(nameof(tracking), "Tracking cannot be null.");
            }

            try
            {
                await _context.Trackings.AddAsync(tracking);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException("An error occurred while adding tracking.", ex);
            }
        }
    }
}