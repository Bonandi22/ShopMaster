using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review, int>
    {
        // Métodos específicos de Review, se necessário
    }
}