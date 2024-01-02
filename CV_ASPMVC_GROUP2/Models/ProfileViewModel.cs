using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class ProfileViewModel
    {
        public User user { get; set; }
        public Cv cv { get; set; }


        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
