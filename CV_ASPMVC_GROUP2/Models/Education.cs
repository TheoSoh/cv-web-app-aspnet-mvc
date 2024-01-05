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
        public string Name { get; set; }

        [Required(ErrorMessage = "Vänligen skriv en beskrivning.")]
        [DisplayName("utbildningsbeskrivning")]
        public string Description { get; set; }
        public virtual IEnumerable<CvEducation> CvEducations { get; set; } = new List<CvEducation>();
    }
}
