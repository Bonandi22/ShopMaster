using AutoMapper;
using ShippingService.Application.DTOs;
using ShippingService.Application.Interfaces;
using ShippingService.Domain.Entities;
using ShippingService.Domain.Interfaces;

namespace ShippingService.Application.Services
{
    public class ShippingsService : IShippingService
    {
        private readonly IShippingRepository _shippingRepository;
        private readonly IMapper _mapper;

        public ShippingsService(IShippingRepository shippingRepository, IMapper mapper)
        {
            _shippingRepository = shippingRepository ?? throw new ArgumentNullException(nameof(shippingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ShippingDto>> GetAllAsync()
        {
            try
            {
                var shippings = await _shippingRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ShippingDto>>(shippings);
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException("An error occurred while retrieving all shippings.", ex);
            }
        }

        public async Task<ShippingDto> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid shipping ID.", nameof(id));
            }

            try
            {
                var shipping = await _shippingRepository.GetByIdAsync(id);
                if (shipping == null)
                {
                    throw new KeyNotFoundException($"Shipping with ID {id} not found.");
                }
                return _mapper.Map<ShippingDto>(shipping);
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException($"An error occurred while retrieving the shipping with ID {id}.", ex);
            }
        }

        public async Task AddAsync(ShippingDto shippingDto)
        {
            if (shippingDto == null)
            {
                throw new ArgumentNullException(nameof(shippingDto), "Shipping data cannot be null.");
            }

            try
            {
                var shipping = _mapper.Map<Shipping>(shippingDto);
                await _shippingRepository.AddAsync(shipping);
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException("An error occurred while adding the shipping.", ex);
            }
        }

        public async Task UpdateAsync(ShippingDto shippingDto)
        {
            if (shippingDto == null)
            {
                throw new ArgumentNullException(nameof(shippingDto), "Shipping data cannot be null.");
            }

            try
            {
                var shipping = _mapper.Map<Shipping>(shippingDto);
                var existingShipping = await _shippingRepository.GetByIdAsync(shipping.Id);
                if (existingShipping == null)
                {
                    throw new KeyNotFoundException($"Shipping with ID {shipping.Id} not found.");
                }
                await _shippingRepository.UpdateAsync(shipping);
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException("An error occurred while updating the shipping.", ex);
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
                var shipping = await _shippingRepository.GetByIdAsync(id);
                if (shipping == null)
                {
                    throw new KeyNotFoundException($"Shipping with ID {id} not found.");
                }
                await _shippingRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Implement logging as needed
                throw new ApplicationException($"An error occurred while deleting the shipping with ID {id}.", ex);
            }
        }
    }
}