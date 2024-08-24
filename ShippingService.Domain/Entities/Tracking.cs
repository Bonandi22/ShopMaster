using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.Domain.Entities
{
    public class Tracking
    {
        public Guid Id { get; set; }
        public Guid? ShippingId { get; set; }
        public Shipping? Shipping { get; set; }
        public string? Location { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}