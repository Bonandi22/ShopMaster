using CatalogService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogService.Application.Interfaces
{
    public interface IProductService : IBaseService<ProductDto, Guid>
    {
    }
}