using ShippingService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.Domain.Interfaces
{
    public interface IShippingRepository
    {
        Task<Shipping> GetByIdAsync(Guid id);

        Task<IEnumerable<Shipping>> GetAllAsync();

        Task AddAsync(Shipping shipping);

        Task UpdateAsync(Shipping shipping);

        Task DeleteAsync(Guid id);
    }
}