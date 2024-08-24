using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.Entities
{
    public class PaymentMethod
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } // e.g., "Credit Card", "PayPal"
        public string? Provider { get; set; } // e.g., "Visa", "Mastercard", "PayPal"
    }
}