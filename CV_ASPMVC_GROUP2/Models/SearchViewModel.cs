namespace CV_ASPMVC_GROUP2.Models
{
    public class SearchViewModel
    {
        public IEnumerable<UserSearchViewModel> UserModels { get; set; }
        public IEnumerable<Competence> AllFilteredCompetences { get; set; }
    }
}