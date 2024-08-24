using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category, Guid>
    {
        // Métodos específicos de Category, se necessário
    }
}