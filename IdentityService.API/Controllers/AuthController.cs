using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for registration attempt.");
                return BadRequest(ModelState);
            }

            try
            {
                var token = await _authService.RegisterAsync(registerDto);
                if (token == null)
                {
                    _logger.LogWarning("User registration failed for user: {UserName}.", registerDto.UserName);
                    return BadRequest("User registration failed.");
                }

                _logger.LogInformation("User {UserName} registered successfully.", registerDto.UserName);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during registration for user: {UserName}.", registerDto.UserName);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for login attempt.");
                return BadRequest(ModelState);
            }

            try
            {
                var token = await _authService.LoginAsync(loginDto);
                if (token == null)
                {
                    _logger.LogWarning("Invalid login attempt for user: {UserName}.", loginDto.UserName);
                    return Unauthorized("Invalid login attempt.");
                }

                _logger.LogInformation("User {UserName} logged in successfully.", loginDto.UserName);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogWarning("Unauthorized access attempt by user: {UserName}.", loginDto.UserName!);
                return Unauthorized("Invalid login attempt.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for user: {UserName}.", loginDto.UserName);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}