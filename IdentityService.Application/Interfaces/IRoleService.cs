using IdentityService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();

        Task<RoleDto> GetByIdAsync(Guid id);

        Task AddAsync(RoleDto roleDto);

        Task UpdateAsync(RoleDto roleDto);

        Task DeleteAsync(Guid id);
    }
}