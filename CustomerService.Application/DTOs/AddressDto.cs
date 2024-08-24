namespace CustomerService.Application.DTOs
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public Guid CustomerId { get; set; }     // Identificador do cliente associado ao endereço

        // Outros campos necessários
    }
}