using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace CV_ASPMVC_GROUP2.Models
{
    public class CvViewModel
    {
        
        [Required(ErrorMessage = "Vänligen skriv en beskrivning av projektet.")]
        public string Description { get; set; }

        public int Id { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public List<EducationViewModel> AvailableEducations { get; set; }

        public List<int> SelectedEducations { get; set; }
        //public Education Education { get; set; }   
        
        public List<ExperienceViewModel> AvailableExperience { get; set; }
        public List<int> SelectedExperience { get; set; }
        //public Experience Experience { get; set; }

        public List<CompetenceViewModel> AvailableCompetence { get; set; }
        public List<int> SelectedCompetence { get; set; }
        //public Competence Competence { get; set; }
    }
}
