using PaymentService.Application.DTOs;

namespace PaymentService.Application.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<PaymentMethodDto> GetByIdAsync(Guid id);

        Task<IEnumerable<PaymentMethodDto>> GetAllAsync();

        Task AddAsync(PaymentMethodDto methodDto);

        Task UpdateAsync(PaymentMethodDto methodDto);

        Task DeleteAsync(Guid id);
    }
}