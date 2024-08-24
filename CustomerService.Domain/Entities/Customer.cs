using System;
using System.Collections.Generic;

namespace CustomerService.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string? Name { get; set; } // Nome completo do cliente

        public string? Email { get; set; } // Email do cliente

        public CustomerStatus Status { get; set; } // Status do cliente

        public ICollection<Address> Addresses { get; set; } = new List<Address>(); // Lista de endereços do cliente
    }
}