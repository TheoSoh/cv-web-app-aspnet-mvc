using System.ComponentModel.DataAnnotations;

namespace CV_ASPMVC_GROUP2.Models
{
    public class CompetenceViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Vänligen fyll i ett Namn.")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vänligen skriv en beskrivning.")]
        public string Description { get; set; }
    }
}
