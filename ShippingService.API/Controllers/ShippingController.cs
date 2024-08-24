using Microsoft.AspNetCore.Mvc;
using ShippingService.Application.DTOs;
using ShippingService.Application.Interfaces;

namespace ShippingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingService _shippingService;

        public ShippingController(IShippingService shippingService)
        {
            _shippingService = shippingService ?? throw new ArgumentNullException(nameof(shippingService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingDto>>> GetAll()
        {
            try
            {
                var shippings = await _shippingService.GetAllAsync();
                return Ok(shippings);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingDto>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid shipping ID.");
            }

            try
            {
                var shipping = await _shippingService.GetByIdAsync(id);
                if (shipping == null)
                {
                    return NotFound();
                }
                return Ok(shipping);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ShippingDto shippingDto)
        {
            if (shippingDto == null)
            {
                return BadRequest("Shipping data is null.");
            }

            try
            {
                await _shippingService.AddAsync(shippingDto);
                return CreatedAtAction(nameof(GetById), new { id = shippingDto.Id }, shippingDto);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] ShippingDto shippingDto)
        {
            if (id == Guid.Empty || shippingDto == null)
            {
                return BadRequest("Invalid shipping data.");
            }

            if (id != shippingDto.Id)
            {
                return BadRequest("Shipping ID mismatch.");
            }

            try
            {
                var existingShipping = await _shippingService.GetByIdAsync(id);
                if (existingShipping == null)
                {
                    return NotFound();
                }

                await _shippingService.UpdateAsync(shippingDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid shipping ID.");
            }

            try
            {
                var existingShipping = await _shippingService.GetByIdAsync(id);
                if (existingShipping == null)
                {
                    return NotFound();
                }

                await _shippingService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}