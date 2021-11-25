using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DOT_NET_Core_Email_System.ViewModels
{
    public class ComposeViewModel
    {
        [Required]
        public int Id { get; set; }
        [EmailAddress]
        public string ToUserEmailId { get; set; }
        public string EmailSubject { get; set; }
        public string EmailText { get; set; }
        [NotMapped]
        public IFormFile Attachment { get; set; }
    }
}
