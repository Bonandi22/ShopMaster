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
    public class CustomersService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ICustomerRepository customerRepository, IMapper mapper, ILogger<CustomersService> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CustomerDto> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogWarning("GetByIdAsync called with an empty GUID.");
                throw new ArgumentException("Invalid customer ID.", nameof(id));
            }

            try
            {
                var customer = await _customerRepository.GetByIdAsync(id);
                if (customer == null)
                {
                    _logger.LogInformation($"Customer with ID {id} not found.");
                    return null;
                }

                return _mapper.Map<CustomerDto>(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving customer with ID {id}.");
                throw;
            }
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CustomerDto>>(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all customers.");
                throw;
            }
        }

        public async Task AddAsync(CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                throw new ArgumentNullException(nameof(customerDto), "Customer data cannot be null.");
            }

            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                await _customerRepository.AddAsync(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new customer.");
                throw;
            }
        }

        public async Task UpdateAsync(CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                throw new ArgumentNullException(nameof(customerDto), "Customer data cannot be null.");
            }

            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                var existingCustomer = await _customerRepository.GetByIdAsync(customerDto.Id);
                if (existingCustomer == null)
                {
                    throw new KeyNotFoundException($"Customer with ID {customerDto.Id} not found.");
                }

                await _customerRepository.UpdateAsync(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a customer.");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid customer ID.", nameof(id));
            }

            try
            {
                var existingCustomer = await _customerRepository.GetByIdAsync(id);
                if (existingCustomer == null)
                {
                    throw new KeyNotFoundException($"Customer with ID {id} not found.");
                }

                await _customerRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a customer.");
                throw;
            }
        }
    }
}