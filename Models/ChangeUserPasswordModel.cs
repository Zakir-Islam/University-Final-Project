using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University_Final_Project.Models
{
    public class ChangeUserPasswordModel
    {
        [Required]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string currentPassword { get; set; }
        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Password does not matched")]
        public string confirmNewPassword { get; set; }
    }
}
