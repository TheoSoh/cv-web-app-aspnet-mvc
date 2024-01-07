using System.ComponentModel.DataAnnotations;

namespace CV_ASPMVC_GROUP2.Models
{
    public class EditUserViewModel
    {
        [RegularExpression(@"^[\p{L}\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string FirstName {  get; set; }

        [RegularExpression(@"^[\p{L}\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string LastName { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Telefonnumret får endast innehålla siffror.")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
        public string Email { get; set; }

        public bool Private { get; set; }

    }
}
