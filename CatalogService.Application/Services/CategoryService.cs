using AutoMapper;
using CatalogService.Application.DTOs;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category, int> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IBaseRepository<Category, int> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    throw new KeyNotFoundException($"Category with ID {id} not found.");
                }
                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error retrieving category.", ex);
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CategoryDto>>(categories);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error retrieving categories.", ex);
            }
        }

        public async Task AddAsync(CategoryDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Category data cannot be null.");
            }

            try
            {
                var category = _mapper.Map<Category>(dto);
                await _categoryRepository.AddAsync(category);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error adding category.", ex);
            }
        }

        public async Task UpdateAsync(CategoryDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Category data cannot be null.");
            }

            try
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(dto.Id);
                if (existingCategory == null)
                {
                    throw new KeyNotFoundException($"Category with ID {dto.Id} not found.");
                }

                var category = _mapper.Map<Category>(dto);
                await _categoryRepository.UpdateAsync(category);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error updating category.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(id);
                if (existingCategory == null)
                {
                    throw new KeyNotFoundException($"Category with ID {id} not found.");
                }

                await _categoryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error deleting category.", ex);
            }
        }

        public Task AddAsync(CategoryDto dto, IFormFile image)
        {
            throw new NotImplementedException();
        }
    }
}