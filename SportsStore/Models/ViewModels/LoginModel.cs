using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace SportsStore.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        //[RegularExpression("^[a-zA-Z0-9].{4,126}$",ErrorMessage = "Patern must be ^[a-zA-Z0-9].{4,126}$")]
        public string Name { get; set; }
        [Required]
        //[RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$% ^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$",
        //    ErrorMessage ="at least a digit, a small leter, a capital leter, a symbol and 8-32 characters in total")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
