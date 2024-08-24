namespace CustomerService.API.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } // Nome completo do cliente
        public string? Email { get; set; }
        public string? Status { get; set; } // Status (Ex: Ativo, Inativo)
        public IEnumerable<AddressDto>? Addresses { get; set; } // Lista de endereços
    }
}