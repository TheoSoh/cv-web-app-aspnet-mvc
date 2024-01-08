using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace CV_ASPMVC_GROUP2.Models
{
    [Serializable]
    public class Competence
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vänligen fyll i ett Namn.")]
        [StringLength(255)]
        [DisplayName("Namn på kompetens")]
        [RegularExpression(@"^[\p{L}\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vänligen skriv en beskrivning.")]
        [DisplayName("Kompetensbeskrivning")]
        [RegularExpression(@"^[\p{L}\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string Description { get; set; }
        [XmlIgnore]
        public virtual IEnumerable<CvCompetence> CvCompetences { get; set; } = new List<CvCompetence>();
        
    }
}
