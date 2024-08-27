using AutoMapper;
using CatalogService.Application.DTOs;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using CatalogService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogService.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product, int> _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ProductService(IBaseRepository<Product, int> productRepository,
                              ICategoryRepository categoryRepository,
                              IMapper mapper,
                              IFileService fileService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
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

            var product = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(product);
        }

        public async Task AddAsync(ProductDto dto, IFormFile image)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Product data cannot be null.");
            }

            // Verifique se o CategoryId é nulo antes de validar
            if (dto.CategoryId != null)
            {
                var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);
                if (!categoryExists)
                {
                    throw new ArgumentException("Invalid CategoryId. The category does not exist.");
                }
            }

            try
            {
                if (image != null)
                {
                    var imagePath = await _fileService.UploadFileAsync(image);
                    dto.ImagePath = imagePath;
                }

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

        public async Task DeleteAsync(int id)
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