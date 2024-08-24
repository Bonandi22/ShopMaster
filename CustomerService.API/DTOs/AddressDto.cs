using System;

namespace CustomerService.API.DTOs
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public Guid? CustomerId { get; set; }     // Identificador do cliente associado ao endereço
    }
}