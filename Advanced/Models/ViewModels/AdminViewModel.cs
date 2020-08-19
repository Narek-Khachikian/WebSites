using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advanced.Models.ViewModels
{
    public class AdminViewModel
    {

        public IEnumerable<IdentityUser> Users { get; set; }
    }
}
