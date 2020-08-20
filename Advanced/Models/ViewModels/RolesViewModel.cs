using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advanced.Models.ViewModels
{
    public class RolesViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public Dictionary<string, string> RoleMembers { get; set; } = new Dictionary<string, string>();
        public IList<IdentityUser> Members { get; set; }
        public IList<IdentityUser> nonMembers { get; set; }
        public IdentityRole Role { get; set; }
    }
}
