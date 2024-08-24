using CustomerService.Application.DTOs;

namespace CustomerService.Application.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAllAsync();

        Task<AddressDto> GetByIdAsync(Guid id);

        Task AddAsync(AddressDto addressDto);

        Task UpdateAsync(AddressDto addressDto);

        Task DeleteAsync(Guid id);
    }
}