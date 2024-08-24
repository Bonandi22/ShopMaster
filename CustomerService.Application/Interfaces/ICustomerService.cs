using CustomerService.Application.DTOs;

namespace CustomerService.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();

        Task<CustomerDto> GetByIdAsync(Guid id);

        Task AddAsync(CustomerDto customerDto);

        Task UpdateAsync(CustomerDto customerDto);

        Task DeleteAsync(Guid id);
    }
}