using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public Cv Cv { get; set; }


        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
