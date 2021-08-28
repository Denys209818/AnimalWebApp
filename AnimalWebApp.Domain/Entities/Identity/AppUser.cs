using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalWebApp.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<long>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
