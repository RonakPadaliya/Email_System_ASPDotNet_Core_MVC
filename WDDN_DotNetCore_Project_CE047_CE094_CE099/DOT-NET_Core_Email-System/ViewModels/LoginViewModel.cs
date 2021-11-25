using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DOT_NET_Core_Email_System.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Key]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
