using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASPMVC_GROUP2.Models
{
    [Serializable]
    public class CvEducation
    {
        public int CvId { get; set; }
        public int EducationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey(nameof(CvId))]
        [XmlIgnore]
        public virtual Cv? Cv { get; set; }

        [ForeignKey(nameof(EducationId))]
        [XmlIgnore]
        public virtual Education? Education { get; set; }
        
    }
}
