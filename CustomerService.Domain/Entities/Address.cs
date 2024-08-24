using System;

namespace CustomerService.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }

        public string? Street { get; set; } // Rua ou avenida

        public string? City { get; set; } // Cidade

        public string? State { get; set; } // Estado

        public string? PostalCode { get; set; } // Código postal

        public string? Country { get; set; } // País

        public Guid? CustomerId { get; set; } // Chave estrangeira para Customer

        public Customer? Customer { get; set; } // Navegação para Customer
    }
}