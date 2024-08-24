using AutoMapper;
using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using IdentityService.Crosscutting.Configuration;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper,
            IOptions<JwtSettings> jwtSettings,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        public async Task<string?> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var user = _mapper.Map<User>(registerDto);
                var result = await _userManager.CreateAsync(user, registerDto.Password!);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Registration failed for user {UserName}: {Errors}", registerDto.UserName, errors);
                    throw new InvalidOperationException($"Registration failed: {errors}");
                }

                _logger.LogInformation("User {UserName} registered successfully.", registerDto.UserName);
                return GenerateJwtToken(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the registration process for user {UserName}.", registerDto.UserName);
                throw;
            }
        }

        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Login attempt failed for user {UserName}.", loginDto.UserName);
                    throw new UnauthorizedAccessException("Invalid login attempt.");
                }

                var user = await _userManager.FindByNameAsync(loginDto.UserName!);
                if (user == null)
                {
                    _logger.LogWarning("User {UserName} not found after successful login attempt.", loginDto.UserName);
                    throw new UnauthorizedAccessException("User not found.");
                }

                _logger.LogInformation("User {UserName} logged in successfully.", loginDto.UserName);
                return GenerateJwtToken(user);
            }
            catch (UnauthorizedAccessException uae)
            {
                _logger.LogWarning(uae, "Unauthorized access during login for user {UserName}.", loginDto.UserName);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the login process for user {UserName}.", loginDto.UserName);
                throw;
            }
        }

        private string GenerateJwtToken(User user)
        {
            try
            {
                var claims = GetClaims(user);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _jwtSettings.Issuer,
                    _jwtSettings.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the JWT token.");
                throw; // Handle or rethrow as necessary.
            }
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            return new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
        }
    }
}