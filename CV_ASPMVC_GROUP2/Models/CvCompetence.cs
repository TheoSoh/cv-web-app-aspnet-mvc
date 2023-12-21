using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class CvCompetence
    {
        public int CvId { get; set; }
        public int CompetenceId { get; set; }

        [ForeignKey(nameof(CvId))]
        public virtual Cv? Cv { get; set; }

        [ForeignKey(nameof(CompetenceId))]
        public virtual Competence? Competence { get; set; }
    }
}
