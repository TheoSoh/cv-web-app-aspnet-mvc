using System.ComponentModel.DataAnnotations;

namespace CV_ASPMVC_GROUP2.Models
{
    public class ProjectViewModel
    {
        [Required(ErrorMessage = "Vänligen skriv en titel.")]
        [StringLength(255)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vänligen skriv en beskrivning av projektet.")]
        public string Description{ get; set; }


        public IFormFile Image {  get; set; } 
    }
}
