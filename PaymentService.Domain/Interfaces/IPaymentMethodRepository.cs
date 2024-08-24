using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.Interfaces
{
    public interface IPaymentMethodRepository
    {
        Task<PaymentMethod> GetByIdAsync(Guid id);

        Task<IEnumerable<PaymentMethod>> GetAllAsync();

        Task AddAsync(PaymentMethod paymentMethod);

        Task UpdateAsync(PaymentMethod paymentMethod);

        Task DeleteAsync(Guid id);
    }
}