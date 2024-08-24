using ShippingService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.Domain.Entities
{
    public class Shipping
    {
        public Guid Id { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public ShippingStatus Status { get; set; }
        public DateTime ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }
}