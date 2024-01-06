using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CV_ASPMVC_GROUP2.Models
{
    public class Competence
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vänligen fyll i ett Namn.")]
        [StringLength(255)]
        [DisplayName("Namn på kompetens")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vänligen skriv en beskrivning.")]
        [DisplayName("Kompetensbeskrivning")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string Description { get; set; }
        public virtual IEnumerable<CvCompetence> CvCompetences { get; set; } = new List<CvCompetence>();
    }
}
