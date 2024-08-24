using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OrderService.Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public OrderDbContext()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais para as entidades, se necessário
        }
    }
}