namespace CV_ASPMVC_GROUP2.Models
{
    public class ShowMessagesViewModel
    {
        public Message Message { get; set; }
        public string FromAnonymousName { get; set; }
        public string FromUserName { get; set;}
        public bool DeleteMessage { get; set; } = false;
    }
}
