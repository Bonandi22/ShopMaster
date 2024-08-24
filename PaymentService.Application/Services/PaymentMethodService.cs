using AutoMapper;
using PaymentService.Application.DTOs;
using PaymentService.Application.Interfaces;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<PaymentMethodDto> GetByIdAsync(Guid id)
        {
            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(id);
            return _mapper.Map<PaymentMethodDto>(paymentMethod);
        }

        public async Task<IEnumerable<PaymentMethodDto>> GetAllAsync()
        {
            var paymentMethods = await _paymentMethodRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentMethodDto>>(paymentMethods);
        }

        public async Task AddAsync(PaymentMethodDto paymentMethodDto)
        {
            var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodDto);
            await _paymentMethodRepository.AddAsync(paymentMethod);
        }

        public async Task UpdateAsync(PaymentMethodDto paymentMethodDto)
        {
            var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodDto);
            await _paymentMethodRepository.UpdateAsync(paymentMethod);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _paymentMethodRepository.DeleteAsync(id);
        }
    }
}