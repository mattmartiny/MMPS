using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MMPS.UI.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "**Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "**Email is Required")]
        [EmailAddress(ErrorMessage = "*Enter a valid Email Address*")]
        public string Email { get; set; }

        [Required(ErrorMessage = "**Subject is Required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "**Message is Required")]
        public string Message { get; set; }

        public string PhoneNumber { get; set; }
    }
}