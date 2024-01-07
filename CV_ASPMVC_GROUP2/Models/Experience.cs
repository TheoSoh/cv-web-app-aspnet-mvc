using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CV_ASPMVC_GROUP2.Models
{
    public class Experience
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vänligen fyll i ett Namn.")]
        [StringLength(255)]
        [DisplayName("Titel på erfarenhet")]
        [RegularExpression(@"^[\p{L}\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vänligen skriv en beskrivning.")]
        [DisplayName("Erfarenhetsbeskrivning")]
        [RegularExpression(@"^[\p{L}\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string Description { get; set; }
        public virtual IEnumerable<CvExperience> Experiences { get; set; } = new List<CvExperience>();
    }
}
