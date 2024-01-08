using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASPMVC_GROUP2.Models
{
    [Serializable]
    public class Cv
    {
        public int Id { get; set; }

        public  string? CvImage { get; set; }

        [Required(ErrorMessage = "Vänligen skriv en beskrivning.")]
        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [NotMapped]
        [DisplayName("Bild")]
        [XmlIgnore]
        public IFormFile? ImageFile { get; set; }
        public String? User_ID { get; set; }
        public int? TotalVisitors { get; set; } = 0;

        [ForeignKey(nameof(User_ID))]
        [XmlIgnore]
        public virtual User? User { get; set; }
        [XmlIgnore]
        public virtual IEnumerable<CvEducation> CvEducations { get; set; } = new List<CvEducation>();
        [XmlIgnore]
        public virtual IEnumerable<CvExperience> CvExperiences { get; set; } = new List<CvExperience>();
        [XmlIgnore]
        public virtual IEnumerable<CvCompetence> CvCompetences { get; set; } = new List<CvCompetence>();
    }
}
