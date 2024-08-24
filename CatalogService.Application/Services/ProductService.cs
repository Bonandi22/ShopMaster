using AutoMapper;
using CatalogService.Application.DTOs;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogService.Application.Services
{
    public class ProductService : IBaseService<ProductDto, Guid>, IProductService
    {
        private readonly IBaseRepository<Product, Guid> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IBaseRepository<Product, Guid> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                }
                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error retrieving product.", ex);
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error retrieving products.", ex);
            }
        }

        public async Task AddAsync(ProductDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Product data cannot be null.");
            }

            try
            {
                var product = _mapper.Map<Product>(dto);
                await _productRepository.AddAsync(product);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error adding product.", ex);
            }
        }

        public async Task UpdateAsync(ProductDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Product data cannot be null.");
            }

            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(dto.Id);
                if (existingProduct == null)
                {
                    throw new KeyNotFoundException($"Product with ID {dto.Id} not found.");
                }

                var product = _mapper.Map<Product>(dto);
                await _productRepository.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error updating product.", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                }

                await _productRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error deleting product.", ex);
            }
        }
    }
}