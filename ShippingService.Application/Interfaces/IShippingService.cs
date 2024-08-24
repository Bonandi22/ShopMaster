using ShippingService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.Application.Interfaces
{
    public interface IShippingService
    {
        Task<IEnumerable<ShippingDto>> GetAllAsync();

        Task<ShippingDto> GetByIdAsync(Guid id);

        Task AddAsync(ShippingDto shippingDto);

        Task UpdateAsync(ShippingDto shippingDto);

        Task DeleteAsync(Guid id);
    }
}