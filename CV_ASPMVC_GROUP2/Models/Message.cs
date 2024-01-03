using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime SentTime { get; set; } = DateTime.Now;
        public bool? Read { get; set; }
        public string? FromUserId { get; set; }
        public string? ToUserId { get; set; }

        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Endast bokstäver tillåtna")]
        public string? FromAnonymousName { get; set; }

        [ForeignKey(nameof(FromUserId))]
        public virtual User? FromUser { get; set; }

        [ForeignKey(nameof(ToUserId))]
        public virtual User? ToUser { get; set; }
    }
}
