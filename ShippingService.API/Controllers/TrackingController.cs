using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingService.Application.DTOs;
using ShippingService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShippingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _trackingService;

        public TrackingController(ITrackingService trackingService)
        {
            _trackingService = trackingService ?? throw new ArgumentNullException(nameof(trackingService));
        }

        [HttpGet("shipping/{shippingId}")]
        public async Task<ActionResult<IEnumerable<TrackingDto>>> GetByShippingId(Guid shippingId)
        {
            if (shippingId == Guid.Empty)
            {
                return BadRequest("Invalid shipping ID.");
            }

            try
            {
                var trackings = await _trackingService.GetByShippingIdAsync(shippingId);
                return Ok(trackings);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TrackingDto trackingDto)
        {
            if (trackingDto == null)
            {
                return BadRequest("Tracking data is null.");
            }

            try
            {
                await _trackingService.AddAsync(trackingDto);
                return CreatedAtAction(nameof(GetByShippingId), new { shippingId = trackingDto.ShippingId }, trackingDto);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}