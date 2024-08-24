using PaymentService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentDto> GetByIdAsync(Guid id);

        Task<IEnumerable<PaymentDto>> GetAllAsync();

        Task AddAsync(PaymentDto paymentDto);

        Task UpdateAsync(PaymentDto paymentDto);

        Task DeleteAsync(Guid id);
    }
}