namespace ShippingService.API.DTOs
{
    public class TrackingDto
    {
        public Guid? Id { get; set; }
        public Guid? ShippingId { get; set; }
        public string? Location { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}