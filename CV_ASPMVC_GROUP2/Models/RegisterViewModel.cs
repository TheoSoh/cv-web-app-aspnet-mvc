using System.ComponentModel.DataAnnotations;

namespace CV_ASPMVC_GROUP2.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vänligen skriv ett användarnamn.")]
        [StringLength(255)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vänligen skriv ett lösenord.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vänlingen bekräfta lösenordet")]
        [DataType(DataType.Password)]
        [Display(Name = "Bekrafta lösenordet")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Vänligen skriv ett förnamn")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Vänligen skriv ett efternamn")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Vänligen skriv in ett telefonnummer")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vänligen skriv in en epost")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
