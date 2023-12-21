using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class CvEducation
    {
        public int CvId { get; set; }
        public int EducationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey(nameof(CvId))]
        public virtual Cv? Cv { get; set; }

        [ForeignKey(nameof(EducationId))]
        public virtual Education? Education { get; set; }
    }
}
