using PaymentService.Domain.ValueObjects;

namespace PaymentService.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string? Currency { get; set; }
        public DateTime PaymentDate { get; set; }

        public Guid PaymentMethodId { get; set; }  // Foreign Key
        public PaymentMethod? PaymentMethod { get; set; } // Navigation Property
    }
}