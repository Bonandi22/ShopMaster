using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product, int>
    {
    }
}