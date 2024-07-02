using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Common;

namespace Zoro.Domain.Models
{
    public class ContactusData:BaseModel
    {
        [Required]
        [Display(Name = "Your Name")]
        public string YourName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Your Email")]
        public string YourEmail { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string YourPhone { get; set; }


        [Required]
        [Display(Name = "Your Message")]
        public string YourMessage { get; set; }


    }
}
