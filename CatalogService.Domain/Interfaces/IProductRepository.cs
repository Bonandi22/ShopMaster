using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product, Guid>
    {
        // Aqui você pode definir métodos específicos do Product, se necessário
    }
}