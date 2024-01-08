using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASPMVC_GROUP2.Models
{
    [Serializable]
    public class CvCompetence
    {
        public int CvId { get; set; }
        public int CompetenceId { get; set; }

        [ForeignKey(nameof(CvId))]
        [XmlIgnore]
        public virtual Cv? Cv { get; set; }

        [ForeignKey(nameof(CompetenceId))]
        [XmlIgnore]
        public virtual Competence? Competence { get; set; }
        
    }
}
