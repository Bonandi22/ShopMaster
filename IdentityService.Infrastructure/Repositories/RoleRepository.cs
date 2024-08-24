using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces;
using IdentityService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityService.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IdentityDbContext _context;

        public RoleRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Roles.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error retrieving role by ID.", ex);
            }
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            try
            {
                return await _context.Roles.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error retrieving all roles.", ex);
            }
        }

        public async Task AddAsync(Role role)
        {
            try
            {
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error adding new role.", ex);
            }
        }

        public async Task UpdateAsync(Role role)
        {
            try
            {
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency conflicts
                throw new Exception("Concurrency conflict while updating role.", ex);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error updating role.", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var role = await _context.Roles.FindAsync(id);
                if (role == null)
                {
                    throw new KeyNotFoundException($"Role with ID {id} not found.");
                }

                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error deleting role.", ex);
            }
        }
    }
}