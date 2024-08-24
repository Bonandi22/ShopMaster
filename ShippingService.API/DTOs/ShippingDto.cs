namespace ShippingService.API.DTOs
{
    public class ShippingDto
    {
        public Guid Id { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public string? Status { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }
}