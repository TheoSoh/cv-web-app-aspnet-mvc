using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASPMVC_GROUP2.Models
{
    [Serializable]
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Förnamn är obligatoriskt.")]
        [RegularExpression(@"^[\p{L}\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn är obligatoriskt.")]
        [RegularExpression(@"^[\p{L}\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string? LastName { get; set; }
        public string? ProfilePicture {  get; set; }
        public bool PrivateStatus { get; set; } = false;
        public bool IsDeactivated { get; set; } = false;
        [XmlIgnore]
        public virtual Cv? Cv { get; set; }
        [XmlIgnore]
        public virtual Address? Address { get; set; }

        [NotMapped]
        [XmlIgnore]
        public IFormFile? ImageFile { get; set; }
        [XmlIgnore]
        public virtual IEnumerable<Project> ProjectsCreated { get; set; } = new List<Project>();
        [XmlIgnore]
        public virtual IEnumerable<Message> SentMessages { get; set; } = new List<Message>();
        [XmlIgnore]
        public virtual IEnumerable<Message> RecievedMessages { get; set; } = new List<Message>();
        [XmlIgnore]
        public virtual IEnumerable<UserProject> UserProjects { get; set; } = new List<UserProject>();
        
    }
}
