using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                _logger.LogInformation("Retrieved all users successfully.");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving users.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} was not found.", id);
                    return NotFound($"User with ID {id} was not found.");
                }

                _logger.LogInformation("Retrieved user with ID {UserId} successfully.", id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user with ID {UserId}.", id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    _logger.LogWarning("Create operation failed due to null UserDto.");
                    return BadRequest("User data cannot be null.");
                }

                await _userService.AddAsync(userDto);
                _logger.LogInformation("User with ID {UserId} was created successfully.", userDto.Id);

                return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new user.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UserDto userDto)
        {
            try
            {
                if (id != userDto.Id)
                {
                    _logger.LogWarning("Update operation failed due to ID mismatch. Route ID: {RouteId}, UserDto ID: {UserDtoId}.", id, userDto.Id);
                    return BadRequest("ID in URL does not match ID in user data.");
                }

                await _userService.UpdateAsync(userDto);
                _logger.LogInformation("User with ID {UserId} was updated successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user with ID {UserId}.", id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                _logger.LogInformation("User with ID {UserId} was deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user with ID {UserId}.", id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }
    }
}