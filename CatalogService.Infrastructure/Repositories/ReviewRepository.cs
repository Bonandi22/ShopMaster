using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using CatalogService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly CatalogDbContext _context;

        public ReviewRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Review entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _context.Reviews.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) throw new KeyNotFoundException($"Review with ID {id} not found.");

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews.AsNoTracking().ToListAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            var review = await _context.Reviews.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            if (review == null) throw new KeyNotFoundException($"Review with ID {id} not found.");

            return review;
        }

        public async Task UpdateAsync(Review entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var existingReview = await _context.Reviews.FindAsync(entity.Id);
            if (existingReview == null) throw new KeyNotFoundException($"Review with ID {entity.Id} not found.");

            _context.Entry(existingReview).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}