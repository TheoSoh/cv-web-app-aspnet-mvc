using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class EditCvViewModel
    {


        [NotMapped]
        [Required(ErrorMessage = "Du måste lägga in en bild till projektet.")]
        public IFormFile? ImageFile { get; set; }

        [RegularExpression(@"^[\p{L}\d]*$", ErrorMessage = "Vänligen ange endast bokstäver och siffror i beskrivningen.")]
        [Required(ErrorMessage = "Vänligen skriv en beskrivning av projektet.")]
        public string Description { get; set; }
    }
}
