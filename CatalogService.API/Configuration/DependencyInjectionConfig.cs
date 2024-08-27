using CatalogService.Application.Interfaces;
using CatalogService.Application.Mapping;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace CatalogService.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do DbContext com MySQL
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 11))));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:issuer"],
                    ValidAudience = configuration["JWT:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:secretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            }
           );

            // Configuração do AutoMapper
            services.AddAutoMapper(typeof(ProductProfile).Assembly);

            // Repositórios
            services.AddScoped<IBaseRepository<Product, int>, ProductRepository>();
            services.AddScoped<IBaseRepository<Category, int>, CategoryRepository>();
            services.AddScoped<IBaseRepository<Review, int>, ReviewRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            // Serviços de Aplicação
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}