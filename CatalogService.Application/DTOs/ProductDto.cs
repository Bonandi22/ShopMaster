﻿using CatalogService.Application.Services;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? ImagePath { get; set; }
    }
}