using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        // Additional properties specific to your application
        public ICollection<UserRole> UserRoles { get; set; }
    }
}