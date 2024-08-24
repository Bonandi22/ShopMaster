using CustomerService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerService.Domain.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> GetByIdAsync(Guid id);

        Task<IEnumerable<Address>> GetAllAsync();

        Task AddAsync(Address address);

        Task UpdateAsync(Address address);

        Task DeleteAsync(Guid id);
    }
}