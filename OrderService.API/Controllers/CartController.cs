using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CartDto>> GetByCustomerId(Guid customerId)
        {
            var cart = await _cartService.GetByCustomerIdAsync(customerId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CartDto cartDto)
        {
            if (cartDto == null)
            {
                return BadRequest();
            }

            await _cartService.AddAsync(cartDto);
            return CreatedAtAction(nameof(GetByCustomerId), new { customerId = cartDto.CustomerId }, cartDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] CartDto cartDto)
        {
            if (id != cartDto.Id)
            {
                return BadRequest();
            }

            var existingCart = await _cartService.GetByIdAsync(id);
            if (existingCart == null)
            {
                return NotFound();
            }

            await _cartService.UpdateAsync(cartDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var cart = await _cartService.GetByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            await _cartService.DeleteAsync(id);
            return NoContent();
        }
    }
}