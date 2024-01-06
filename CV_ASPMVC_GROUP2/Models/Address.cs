using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class Address
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Gatan kan endast innehålla bokstäver och siffror.")]
        public string? Street { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Staden kan endast innehålla bokstäver.")]
        public string? City { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Postnumret kan endast innehålla siffror.")]
        public int? PostalCode { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
    }
}
