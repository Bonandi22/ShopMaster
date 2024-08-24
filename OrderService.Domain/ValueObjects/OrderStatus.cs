namespace OrderService.Domain.ValueObjects
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Canceled,
        Completed
    }
}