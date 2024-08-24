using AutoMapper;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrderService.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartDto> GetByIdAsync(Guid id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetByCustomerIdAsync(Guid customerId)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(customerId);
            return _mapper.Map<CartDto>(cart);
        }

        public async Task AddAsync(CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            await _cartRepository.AddAsync(cart);
        }

        public async Task UpdateAsync(CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            await _cartRepository.UpdateAsync(cart);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _cartRepository.DeleteAsync(id);
        }
    }
}