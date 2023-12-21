namespace CV_ASPMVC_GROUP2.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<CvExperience> Experiences { get; set; } = new List<CvExperience>();
    }
}
