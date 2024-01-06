using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CV_ASPMVC_GROUP2.Models
{
    public class Education
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vänligen fyll i ett Namn.")]
        [StringLength(255)]
        [DisplayName("Namn på utbildning")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vänligen skriv en beskrivning.")]
        [DisplayName("utbildningsbeskrivning")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string Description { get; set; }
        public virtual IEnumerable<CvEducation> CvEducations { get; set; } = new List<CvEducation>();
    }
}
