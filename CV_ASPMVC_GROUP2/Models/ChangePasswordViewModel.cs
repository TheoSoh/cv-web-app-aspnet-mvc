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
        public string? NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
