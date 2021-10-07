using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Challenger.Web.Models
{
    public class PersonsModel
    {
        [Display(Name = "Id")]
        public int CdPerson { get; set; }
        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DtPerson { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name is required")]
        public string NmPerson { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string DsEmail { get; set; }
        [Required(ErrorMessage = "The password is required")]
        [Display(Name = "Password")]
        public string DsPassword { get; set; }
        [Display(Name = "Active")]
        public bool FlActive { get; set; }
    }
}
