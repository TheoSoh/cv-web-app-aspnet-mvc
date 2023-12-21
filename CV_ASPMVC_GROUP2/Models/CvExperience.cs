using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class CvExperience
    {
        public int CvId { get; set; }
        public int ExperienceId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey(nameof(CvId))]
        public virtual Cv? Cv { get; set; }

        [ForeignKey(nameof(ExperienceId))]
        public virtual Experience? Experience { get; set; }
    }
}
