using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class UserProject
    {
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public string? UserRole { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? user { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project? Project { get; set; }
    }
}
