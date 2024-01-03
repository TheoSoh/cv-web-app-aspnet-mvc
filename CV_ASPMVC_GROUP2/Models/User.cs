using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePicture {  get; set; }
        public virtual Cv? Cv { get; set; }
        public virtual Address? Address { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public virtual IEnumerable<Project> ProjectsCreated { get; set; } = new List<Project>();
        public virtual IEnumerable<Message> SentMessages { get; set; } = new List<Message>();
        public virtual IEnumerable<Message> RecievedMessages { get; set; } = new List<Message>();
        public virtual IEnumerable<UserProject> UserProjects { get; set; } = new List<UserProject>();
    }
}
