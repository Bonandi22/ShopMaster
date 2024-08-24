using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.Domain.ValueObjects
{
    public enum ShippingStatus
    {
        Pending,
        Shipped,
        InTransit,
        Delivered,
        Returned
    }
}