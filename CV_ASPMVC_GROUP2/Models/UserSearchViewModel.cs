namespace CV_ASPMVC_GROUP2.Models
{
    public class UserSearchViewModel
    {
        public User AUser { get; set; }
        public IEnumerable<Competence> Competences { get; set; }
        public Cv Cv { get; set; }
    }
}
