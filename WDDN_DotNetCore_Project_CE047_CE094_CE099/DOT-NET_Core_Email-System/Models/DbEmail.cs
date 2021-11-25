using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DOT_NET_Core_Email_System.Models
{
    public class DbEmail
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string FromUserEmailId { get; set; }
        [Required]
        [EmailAddress]
        public string ToUserEmailId { get; set; }
        [Required]
        public string EmailSubject { get; set; }
        [Required]
        public string EmailText { get; set; }
        [Required]
        public Boolean Is_Inbox { get; set; }
        [Required]
        public Boolean Is_Sent { get; set; }
        [Required]
        public Boolean Is_FromUser_Starred { get; set; }
        [Required]
        public Boolean Is_ToUser_Starred { get; set; }
        [Required]
        public Boolean Is_FromUser_Delete { get; set; }
        [Required]
        public Boolean Is_ToUser_Delete { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentType { get; set; }
        public byte[] AttachmentData { get; set; }
    }
}
