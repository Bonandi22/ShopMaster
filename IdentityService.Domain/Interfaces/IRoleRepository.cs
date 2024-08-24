using IdentityService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetByIdAsync(Guid id);

        Task<IEnumerable<Role>> GetAllAsync();

        Task AddAsync(Role role);

        Task UpdateAsync(Role role);

        Task DeleteAsync(Guid id);
    }
}