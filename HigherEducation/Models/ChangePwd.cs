using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class ChangePwd
    {
        public string Regid { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
         ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ConfirmPassword { get; set; }

    }
}