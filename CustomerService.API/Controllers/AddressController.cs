using CustomerService.Application.DTOs;
using CustomerService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        }

        /// <summary>
        /// Retrieves all addresses.
        /// </summary>
        /// <returns>List of addresses.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAll()
        {
            try
            {
                var addresses = await _addressService.GetAllAsync();
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves an address by its ID.
        /// </summary>
        /// <param name="id">The ID of the address.</param>
        /// <returns>The address with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid address ID.");
            }

            try
            {
                var address = await _addressService.GetByIdAsync(id);
                if (address == null)
                {
                    return NotFound($"Address with ID {id} not found.");
                }
                return Ok(address);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new address.
        /// </summary>
        /// <param name="addressDto">The address data to create.</param>
        /// <returns>The created address.</returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return BadRequest("Address data cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _addressService.AddAsync(addressDto);
                return CreatedAtAction(nameof(GetById), new { id = addressDto.Id }, addressDto);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing address.
        /// </summary>
        /// <param name="id">The ID of the address to update.</param>
        /// <param name="addressDto">The updated address data.</param>
        /// <returns>Result of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] AddressDto addressDto)
        {
            if (id == Guid.Empty || addressDto == null)
            {
                return BadRequest("Invalid address data.");
            }

            if (id != addressDto.Id)
            {
                return BadRequest("Address ID mismatch.");
            }

            try
            {
                var existingAddress = await _addressService.GetByIdAsync(id);
                if (existingAddress == null)
                {
                    return NotFound($"Address with ID {id} not found.");
                }

                await _addressService.UpdateAsync(addressDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an address by its ID.
        /// </summary>
        /// <param name="id">The ID of the address to delete.</param>
        /// <returns>Result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid address ID.");
            }

            try
            {
                var address = await _addressService.GetByIdAsync(id);
                if (address == null)
                {
                    return NotFound($"Address with ID {id} not found.");
                }

                await _addressService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}