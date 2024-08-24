using IdentityService.Application.DTOs;

namespace IdentityService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string?> RegisterAsync(RegisterDto registerDto);

        Task<string?> LoginAsync(LoginDto loginDto);
    }
}