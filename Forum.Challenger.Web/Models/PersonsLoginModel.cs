using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Challenger.Web.Models
{
    public class PersonsLoginModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string DsEmail { get; set; }
        [Required(ErrorMessage = "The password is required")]
        [Display(Name = "Password")]
        public string DsPassword { get; set; }
    }
}
