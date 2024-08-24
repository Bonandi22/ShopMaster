using AutoMapper;
using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IRoleRepository roleRepository, IMapper mapper, ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RoleDto> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("GetByIdAsync was called with an empty GUID.");
                    throw new ArgumentException("ID cannot be an empty GUID.", nameof(id));
                }

                var role = await _roleRepository.GetByIdAsync(id);

                if (role == null)
                {
                    _logger.LogWarning("Role with ID {RoleId} was not found.", id);
                    return null;
                }

                _logger.LogInformation("Role with ID {RoleId} retrieved successfully.", id);
                return _mapper.Map<RoleDto>(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID {RoleId}.", id);
                throw;
            }
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            try
            {
                var roles = await _roleRepository.GetAllAsync();
                _logger.LogInformation("Roles retrieved successfully.");
                return _mapper.Map<IEnumerable<RoleDto>>(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all roles.");
                throw;
            }
        }

        public async Task AddAsync(RoleDto roleDto)
        {
            try
            {
                if (roleDto == null)
                {
                    _logger.LogWarning("AddAsync was called with a null roleDto.");
                    throw new ArgumentNullException(nameof(roleDto));
                }

                var role = _mapper.Map<Role>(roleDto);
                await _roleRepository.AddAsync(role);
                _logger.LogInformation("Role '{RoleName}' added successfully.", roleDto.RoleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new role.");
                throw;
            }
        }

        public async Task UpdateAsync(RoleDto roleDto)
        {
            try
            {
                if (roleDto == null)
                {
                    _logger.LogWarning("UpdateAsync was called with a null roleDto.");
                    throw new ArgumentNullException(nameof(roleDto));
                }

                var role = _mapper.Map<Role>(roleDto);
                await _roleRepository.UpdateAsync(role);
                _logger.LogInformation("Role '{RoleName}' updated successfully.", roleDto.RoleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating role '{RoleName}'.", roleDto.RoleName);
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("DeleteAsync was called with an empty GUID.");
                    throw new ArgumentException("ID cannot be an empty GUID.", nameof(id));
                }

                await _roleRepository.DeleteAsync(id);
                _logger.LogInformation("Role with ID {RoleId} deleted successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting role with ID {RoleId}.", id);
                throw;
            }
        }
    }
}