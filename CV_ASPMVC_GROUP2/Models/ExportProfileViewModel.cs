using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace CV_ASPMVC_GROUP2.Models
{
    [XmlInclude(typeof(ExportProfileViewModel))]
    [XmlInclude(typeof(User))]
    [XmlInclude(typeof(Address))]
    [XmlInclude(typeof(Cv))]
    [XmlInclude(typeof(CvCompetence))]
    [XmlInclude(typeof(Competence))]
    [XmlInclude(typeof(CvEducation))]
    [XmlInclude(typeof(Education))]
    [XmlInclude(typeof(CvExperience))]
    [XmlInclude(typeof(Experience))]
    [XmlInclude(typeof(Project))]
    public class ExportProfileViewModel
    {
        public User user {  get; set; }
        public Address address { get; set; }
        public Cv cv { get; set; }
        public List<CvCompetence> cvCompetences { get; set; }
        public List<Competence> competences { get; set; }
        public List<CvEducation> cvEducations { get; set; }
        public List<Education> educations { get; set; }
        public List<CvExperience> cvExperiences { get; set; }
        public List<Experience> experiences { get; set; }
        public List<Project> projects { get; set; }
    }
}
