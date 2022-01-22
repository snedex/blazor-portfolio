using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class UserViewModel
    {
        [EmailAddress]
        [Required]
        [Display(Name ="Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Your password must be between {2} and {1} characters", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
