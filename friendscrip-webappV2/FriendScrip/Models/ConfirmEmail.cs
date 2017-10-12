using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FriendScrip.Models
{
    public class ConfirmEmail
    {
        public string ID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string SameEmail { get; set; }
    }
}