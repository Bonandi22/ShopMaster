using Microsoft.EntityFrameworkCore;
using ShippingService.Domain.Entities;

namespace ShippingService.Infrastructure.Data
{
    public class ShippingDbContext : DbContext
    {
        public ShippingDbContext(DbContextOptions<ShippingDbContext> options)
            : base(options)
        {
        }

        public ShippingDbContext()
        { }

        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<Tracking> Trackings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de entidades e relacionamentos

            // Exemplo de configuração para a entidade Shipping
            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.ToTable("Shippings");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.TrackingNumber)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Carrier)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Status)
                      .IsRequired();

                entity.Property(e => e.ShippedDate)
                      .IsRequired();

                entity.Property(e => e.DeliveredDate)
                      .IsRequired(false);
            });

            // Exemplo de configuração para a entidade Tracking
            modelBuilder.Entity<Tracking>(entity =>
            {
                entity.ToTable("Trackings");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Location)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Timestamp)
                      .IsRequired();

                // Configuração de relacionamento
                entity.HasOne(e => e.Shipping)
                      .WithMany()
                      .HasForeignKey(e => e.ShippingId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}