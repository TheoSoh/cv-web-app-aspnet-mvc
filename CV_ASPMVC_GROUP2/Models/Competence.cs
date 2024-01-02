namespace CV_ASPMVC_GROUP2.Models
{
    public class Competence
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public virtual IEnumerable<CvCompetence> CvCompetences { get; set; } = new List<CvCompetence>();
    }
}
