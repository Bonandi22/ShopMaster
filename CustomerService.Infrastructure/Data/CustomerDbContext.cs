using CustomerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        public CustomerDbContext()
        { }

        public DbSet<Customer> Customers { get; set; }  // DbSet para clientes
        public DbSet<Address> Addresses { get; set; }   // DbSet para endereços

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais para as entidades, se necessário
        }
    }
}