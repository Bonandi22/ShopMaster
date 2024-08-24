using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.DTOs
{
    public class PaymentMethodDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Details { get; set; }
        public bool? IsEnabled { get; set; }
    }
}