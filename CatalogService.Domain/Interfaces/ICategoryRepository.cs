using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category, int>
    {
        Task<bool> ExistsAsync(int categoryId);
    }
}