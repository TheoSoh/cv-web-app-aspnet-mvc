using System.ComponentModel.DataAnnotations;

namespace CV_ASPMVC_GROUP2.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string? NewPassword { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Bekrafta lösenordet")]
        public string? ConfirmPassword { get; set; }
    }
}
