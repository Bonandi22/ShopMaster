using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.DTOs
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public decimal? Amount { get; set; }
        public string? Status { get; set; }
        public string? Currency { get; set; }
        public DateTime PaymentDate { get; set; }

        public Guid PaymentMethodId { get; set; }
        public PaymentMethodDto? PaymentMethod { get; set; }
    }
}