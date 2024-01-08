using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASPMVC_GROUP2.Models
{
    [Serializable]
    public class CvExperience
    {
        public int CvId { get; set; }
        public int ExperienceId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey(nameof(CvId))]
        [XmlIgnore]
        public virtual Cv? Cv { get; set; }

        [ForeignKey(nameof(ExperienceId))]
        [XmlIgnore]
        public virtual Experience? Experience { get; set; }
        
    }
}
