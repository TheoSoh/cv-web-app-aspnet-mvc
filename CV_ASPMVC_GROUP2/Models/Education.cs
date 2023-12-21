namespace CV_ASPMVC_GROUP2.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<CvEducation> CvEducations { get; set; } = new List<CvEducation>();
    }
}
