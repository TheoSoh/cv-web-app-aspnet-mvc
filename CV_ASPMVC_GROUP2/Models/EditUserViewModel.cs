using System.ComponentModel.DataAnnotations;

namespace CV_ASPMVC_GROUP2.Models
{
    public class EditUserViewModel
    {
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Förnamnet får endast innehålla bokstäver.")]
        public string FirstName {  get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Efternamnet får endast innehålla bokstäver.")]
        public string LastName { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Telefonnumret får endast innehålla siffror.")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
        public string Email { get; set; }

        public bool Private { get; set; }

    }
}
