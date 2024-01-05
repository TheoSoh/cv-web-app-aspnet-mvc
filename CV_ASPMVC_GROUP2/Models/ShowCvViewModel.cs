using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace CV_ASPMVC_GROUP2.Models
{
    public class ShowCvViewModel
    {
        public Cv Cv { get; set; }
        public User User { get; set; }
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Experience> Experiences { get; set; }
        public IEnumerable<Competence> Competences { get; set; }

       
    }
}
