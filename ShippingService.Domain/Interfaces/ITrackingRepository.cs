using ShippingService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.Domain.Interfaces
{
    public interface ITrackingRepository
    {
        Task<Tracking> GetByIdAsync(Guid id);

        Task<IEnumerable<Tracking>> GetByShippingIdAsync(Guid shippingId);

        Task AddAsync(Tracking tracking);
    }
}