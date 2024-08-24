using ShippingService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.Application.Interfaces
{
    public interface ITrackingService
    {
        Task<IEnumerable<TrackingDto>> GetByShippingIdAsync(Guid shippingId);

        Task AddAsync(TrackingDto trackingDto);
    }
}