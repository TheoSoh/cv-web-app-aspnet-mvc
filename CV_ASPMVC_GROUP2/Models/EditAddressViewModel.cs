using System.ComponentModel.DataAnnotations;

namespace CV_ASPMVC_GROUP2.Models
{
    public class EditAddressViewModel
    {
        [Required(ErrorMessage = "Vänligen skriv din gatu-adress och gatu-nummer.")]
        [Display(Name = "Gatu-adress & husnummer")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Vänligen ange stad.")]
        [Display(Name = "Stad")]
        public string City { get; set; }
        [RegularExpression("^\\d+$", ErrorMessage ="Postkoden måste bestå av Siffror.")]
        [Display(Name = "Postkod")]
        [StringLength(5)]
        public string PostalCode { get; set; }
    }
}
