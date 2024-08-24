using AutoMapper;
using ShippingService.Application.DTOs;
using ShippingService.Application.Interfaces;
using ShippingService.Domain.Entities;
using ShippingService.Domain.Interfaces;

namespace ShippingService.Application.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly ITrackingRepository _trackingRepository;
        private readonly IMapper _mapper;

        public TrackingService(ITrackingRepository trackingRepository, IMapper mapper)
        {
            _trackingRepository = trackingRepository ?? throw new ArgumentNullException(nameof(trackingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TrackingDto>> GetByShippingIdAsync(Guid shippingId)
        {
            if (shippingId == Guid.Empty)
            {
                throw new ArgumentException("Invalid shipping ID.", nameof(shippingId));
            }

            try
            {
                var trackings = await _trackingRepository.GetByShippingIdAsync(shippingId);
                return _mapper.Map<IEnumerable<TrackingDto>>(trackings);
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException($"An error occurred while retrieving trackings for shipping ID {shippingId}.", ex);
            }
        }

        public async Task AddAsync(TrackingDto trackingDto)
        {
            if (trackingDto == null)
            {
                throw new ArgumentNullException(nameof(trackingDto), "Tracking data cannot be null.");
            }

            try
            {
                var tracking = _mapper.Map<Tracking>(trackingDto);
                await _trackingRepository.AddAsync(tracking);
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException("An error occurred while adding the tracking information.", ex);
            }
        }
    }
}