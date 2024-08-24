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
    public class ReviewService : IBaseService<ReviewDto, Guid>, IReviewService
    {
        private readonly IBaseRepository<Review, Guid> _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IBaseRepository<Review, Guid> reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<ReviewDto> GetByIdAsync(Guid id)
        {
            try
            {
                var review = await _reviewRepository.GetByIdAsync(id);
                if (review == null)
                {
                    throw new KeyNotFoundException($"Review with ID {id} not found.");
                }
                return _mapper.Map<ReviewDto>(review);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error retrieving review.", ex);
            }
        }

        public async Task<IEnumerable<ReviewDto>> GetAllAsync()
        {
            try
            {
                var reviews = await _reviewRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error retrieving reviews.", ex);
            }
        }

        public async Task AddAsync(ReviewDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Review data cannot be null.");
            }

            try
            {
                var review = _mapper.Map<Review>(dto);
                await _reviewRepository.AddAsync(review);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error adding review.", ex);
            }
        }

        public async Task UpdateAsync(ReviewDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Review data cannot be null.");
            }

            try
            {
                var existingReview = await _reviewRepository.GetByIdAsync(dto.Id);
                if (existingReview == null)
                {
                    throw new KeyNotFoundException($"Review with ID {dto.Id} not found.");
                }

                var review = _mapper.Map<Review>(dto);
                await _reviewRepository.UpdateAsync(review);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error updating review.", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var existingReview = await _reviewRepository.GetByIdAsync(id);
                if (existingReview == null)
                {
                    throw new KeyNotFoundException($"Review with ID {id} not found.");
                }

                await _reviewRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("Error deleting review.", ex);
            }
        }
    }
}