namespace CV_ASPMVC_GROUP2.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public virtual IEnumerable<UserProject> UserProjects { get; set; } = new List<UserProject>();
    }
}
