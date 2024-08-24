using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.DTOs;
using PaymentService.Application.Interfaces;

namespace PaymentService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodsController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethodDto>>> GetAll()
        {
            var paymentMethods = await _paymentMethodService.GetAllAsync();
            return Ok(paymentMethods);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethodDto>> GetById(Guid id)
        {
            var paymentMethod = await _paymentMethodService.GetByIdAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return Ok(paymentMethod);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PaymentMethodDto paymentMethodDto)
        {
            if (paymentMethodDto == null)
            {
                return BadRequest();
            }

            await _paymentMethodService.AddAsync(paymentMethodDto);
            return CreatedAtAction(nameof(GetById), new { id = paymentMethodDto.Id }, paymentMethodDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] PaymentMethodDto paymentMethodDto)
        {
            if (id != paymentMethodDto.Id)
            {
                return BadRequest();
            }

            await _paymentMethodService.UpdateAsync(paymentMethodDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _paymentMethodService.DeleteAsync(id);
            return NoContent();
        }
    }
}