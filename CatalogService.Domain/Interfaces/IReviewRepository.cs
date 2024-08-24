using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review, Guid>
    {
        // Métodos específicos de Review, se necessário
    }
}