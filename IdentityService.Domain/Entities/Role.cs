using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityService.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        public string? RoleName { get; set; }
    }
}