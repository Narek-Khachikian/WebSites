using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Advanced.Models
{
    public class UserEditingViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name ="User Name")]
        [StringLength(maximumLength : 25,MinimumLength =5, ErrorMessage ="User Name should be at least 5 leters long and shorter than 25 leters")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9](\S)*@(\S)+\.\w+$",ErrorMessage ="Email address should folow the 'example@example.any' patern")]
        [StringLength(maximumLength:100, MinimumLength =6)]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength:50)]
        public string Password { get; set; }
    }
}
