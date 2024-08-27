using CatalogService.Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogService.Application.Interfaces
{
    public interface IProductService : IBaseService<ProductDto, int>
    {
        Task AddAsync(ProductDto dto, IFormFile image);
    }
}