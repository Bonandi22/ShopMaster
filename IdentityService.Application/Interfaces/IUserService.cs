using IdentityService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(Guid id);

        Task<IEnumerable<UserDto>> GetAllAsync();

        Task AddAsync(UserDto userDto);

        Task UpdateAsync(UserDto userDto);

        Task DeleteAsync(Guid id);
    }
}