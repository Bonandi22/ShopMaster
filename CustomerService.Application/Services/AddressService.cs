using AutoMapper;
using CustomerService.Application.DTOs;
using CustomerService.Application.Interfaces;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerService.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IAddressRepository addressRepository, IMapper mapper, ILogger<AddressService> logger)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<AddressDto> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogWarning("GetByIdAsync called with an empty GUID.");
                throw new ArgumentException("Invalid address ID.", nameof(id));
            }

            try
            {
                var address = await _addressRepository.GetByIdAsync(id);
                if (address == null)
                {
                    _logger.LogInformation($"Address with ID {id} not found.");
                    return null;
                }

                return _mapper.Map<AddressDto>(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving address with ID {id}.");
                throw;
            }
        }

        public async Task<IEnumerable<AddressDto>> GetAllAsync()
        {
            try
            {
                var addresses = await _addressRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<AddressDto>>(addresses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all addresses.");
                throw;
            }
        }

        public async Task AddAsync(AddressDto addressDto)
        {
            if (addressDto == null)
            {
                throw new ArgumentNullException(nameof(addressDto), "Address data cannot be null.");
            }

            try
            {
                var address = _mapper.Map<Address>(addressDto);
                await _addressRepository.AddAsync(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new address.");
                throw;
            }
        }

        public async Task UpdateAsync(AddressDto addressDto)
        {
            if (addressDto == null)
            {
                throw new ArgumentNullException(nameof(addressDto), "Address data cannot be null.");
            }

            try
            {
                var address = _mapper.Map<Address>(addressDto);
                var existingAddress = await _addressRepository.GetByIdAsync(addressDto.Id);
                if (existingAddress == null)
                {
                    throw new KeyNotFoundException($"Address with ID {addressDto.Id} not found.");
                }

                await _addressRepository.UpdateAsync(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating an address.");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid address ID.", nameof(id));
            }

            try
            {
                var existingAddress = await _addressRepository.GetByIdAsync(id);
                if (existingAddress == null)
                {
                    throw new KeyNotFoundException($"Address with ID {id} not found.");
                }

                await _addressRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an address.");
                throw;
            }
        }
    }
}