namespace CustomerService.Application.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; } // Primeiro nome do cliente
        public string? LastName { get; set; }  // Último nome do cliente
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; } // Número de telefone do cliente
        public string? Status { get; set; } // Status (Ex: Ativo, Inativo)

        public IEnumerable<AddressDto>? Addresses { get; set; } // Lista de endereços
    }
}