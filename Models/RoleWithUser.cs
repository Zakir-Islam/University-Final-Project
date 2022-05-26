using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class RoleWithUser
    {
        public IdentityRole role { get; set; }
      
        public IList<ApplicationUser> users { get; set; }
    }
}
