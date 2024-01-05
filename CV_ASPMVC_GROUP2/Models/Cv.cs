using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class Cv
    {
        public int Id { get; set; }

        public  string? CvImage { get; set; }

        [Required(ErrorMessage = "Vänligen skriv en beskrivning.")]
        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [NotMapped]
        [DisplayName("Bild")]
        public IFormFile? ImageFile { get; set; }
        public String? User_ID { get; set; }

        [ForeignKey(nameof(User_ID))]
        public virtual User? User { get; set; }
        public virtual IEnumerable<CvEducation> CvEducations { get; set; } = new List<CvEducation>();
        public virtual IEnumerable<CvExperience> CvExperiences { get; set; } = new List<CvExperience>();
        public virtual IEnumerable<CvCompetence> CvCompetences { get; set; } = new List<CvCompetence>();
    }
}
