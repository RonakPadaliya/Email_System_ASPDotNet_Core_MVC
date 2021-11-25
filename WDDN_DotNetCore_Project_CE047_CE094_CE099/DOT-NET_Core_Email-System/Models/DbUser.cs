using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DOT_NET_Core_Email_System.Models
{
    public class DbUser
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserFirstName { get; set; }
        [Required]
        public string UserLastName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string UserPass { get; set; }
        [Required]
        [EmailAddress]
        public string UserEmailId { get; set; }
    }
}
