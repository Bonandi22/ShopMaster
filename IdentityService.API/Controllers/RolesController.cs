using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRoleService roleService, ILogger<RolesController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            try
            {
                var roles = await _roleService.GetAllAsync();
                _logger.LogInformation("Retrieved all roles successfully.");
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving roles.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetById(Guid id)
        {
            try
            {
                var role = await _roleService.GetByIdAsync(id);

                if (role == null)
                {
                    _logger.LogWarning("Role with ID {RoleId} was not found.", id);
                    return NotFound($"Role with ID {id} was not found.");
                }

                _logger.LogInformation("Retrieved role with ID {RoleId} successfully.", id);
                return Ok(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID {RoleId}.", id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] RoleDto roleDto)
        {
            try
            {
                if (roleDto == null)
                {
                    _logger.LogWarning("Create operation failed due to null RoleDto.");
                    return BadRequest("Role data cannot be null.");
                }

                await _roleService.AddAsync(roleDto);
                _logger.LogInformation("Role with ID {RoleId} was created successfully.", roleDto.Id);

                return CreatedAtAction(nameof(GetById), new { id = roleDto.Id }, roleDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new role.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] RoleDto roleDto)
        {
            try
            {
                if (id != roleDto.Id)
                {
                    _logger.LogWarning("Update operation failed due to ID mismatch. Route ID: {RouteId}, RoleDto ID: {RoleDtoId}.", id, roleDto.Id);
                    return BadRequest("ID in URL does not match ID in role data.");
                }

                await _roleService.UpdateAsync(roleDto);
                _logger.LogInformation("Role with ID {RoleId} was updated successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating role with ID {RoleId}.", id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _roleService.DeleteAsync(id);
                _logger.LogInformation("Role with ID {RoleId} was deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting role with ID {RoleId}.", id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }
    }
}