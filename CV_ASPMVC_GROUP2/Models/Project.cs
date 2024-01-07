using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vänligen skriv en titel.")]
        [RegularExpression(@"^[\p{L}\d]*$", ErrorMessage = "Vänligen ange endast bokstäver och siffror.")]

        public string? Name { get; set; }

        [Required(ErrorMessage = "Vänligen skriv en beskrivning av projektet.")]
        [RegularExpression(@"^[\p{L}\d]*$", ErrorMessage = "Vänligen ange endast bokstäver och siffror.")]

        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? Image { get; set; }
        public string CreatedByUserId { get; set; }

        [ForeignKey(nameof(CreatedByUserId))]
        public virtual User User { get; set; }

        [NotMapped]
        [Required]
        public IFormFile? ImageFile { get; set; }
        public virtual IEnumerable<UserProject> UserProjects { get; set; } = new List<UserProject>();
    }
}
