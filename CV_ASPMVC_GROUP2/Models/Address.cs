using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public int? City { get; set; }
        public int? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
    }
}
